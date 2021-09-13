using AutoMapper;
using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.ViewModels.Validatables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Contoso.XPlatform.Utils
{
    public static class EntityMapper
    {
        public static Dictionary<string, object> EntityToObjectDictionary(this object entity, IMapper mapper, List<FormItemSettingsDescriptor> fieldSettings)
            => mapper.Map<Dictionary<string, object>>(entity).ToObjectDictionaryFromEntity(mapper, fieldSettings);

        public static Dictionary<string, object> ValidatableListToObjectDictionary(this IEnumerable<IValidatable> properties, IMapper mapper, List<FormItemSettingsDescriptor> fieldSettings)
            => properties.ToDictionary(p => p.Name, p => p.Value).ToObjectDictionaryFromValidatableObjects(mapper, fieldSettings);

        public static object ToModelObject(this IEnumerable<IValidatable> properties, Type entityType, IMapper mapper, List<FormItemSettingsDescriptor> fieldSettings, object destination = null)
        {
            MethodInfo methodInfo = typeof(EntityMapper).GetMethod
            (
                "ToModelObject",
                1,
                new Type[]
                {
                    typeof(IEnumerable<IValidatable>),
                    typeof(IMapper),
                    typeof(List<FormItemSettingsDescriptor>),
                    typeof(object)
                }
            ).MakeGenericMethod(entityType);

            return methodInfo.Invoke(null, new object[] { properties, mapper, fieldSettings, destination });
        }

        public static T ToModelObject<T>(this IEnumerable<IValidatable> properties, IMapper mapper, List<FormItemSettingsDescriptor> fieldSettings, object destination = null)
        {
            if (destination == null)
            {
                return mapper.Map<T>
                (
                    properties.ToDictionary(p => p.Name, p => p.Value).ToObjectDictionaryFromValidatableObjects(mapper, fieldSettings)
                );
            }

            return (T)mapper.Map
            (
                properties.ToDictionary(p => p.Name, p => p.Value).ToObjectDictionaryFromValidatableObjects(mapper, fieldSettings),
                destination,
                typeof(Dictionary<string, object>),
                typeof(T)
            );
        }

        /// <summary>
        /// Ensures all child objects and collections are dictionaries
        /// </summary>
        /// <param name="propertiesDictionary"></param>
        /// <param name="mapper"></param>
        /// <param name="fieldSettings"></param>
        /// <param name="parentField"></param>
        /// <returns></returns>
        private static Dictionary<string, object> ToObjectDictionaryFromValidatableObjects(this IDictionary<string, object> propertiesDictionary, IMapper mapper, List<FormItemSettingsDescriptor> fieldSettings, string parentField = null)
        {
            return fieldSettings.Aggregate(new Dictionary<string, object>(), (objectDictionary, setting) =>
            {
                if (setting is MultiSelectFormControlSettingsDescriptor multiSelectFormControlSetting)
                    AddMultiSelects();
                else if (setting is FormControlSettingsDescriptor controlSetting)
                    AddSingleValueField();//must stay after MultiSelect because MultiSelect extends FormControl
                else if (setting is FormGroupSettingsDescriptor formGroupSetting)
                {
                    if (formGroupSetting.FormGroupTemplate == null)
                        throw new ArgumentException($"{nameof(formGroupSetting.FormGroupTemplate)}: E29413BD-6DC7-47D1-9986-B613E0568AFE");

                    if (formGroupSetting.FormGroupTemplate.TemplateName == FromGroupTemplateNames.InlineFormGroupTemplate)
                        AddFormGroupInline(formGroupSetting);
                    else if (formGroupSetting.FormGroupTemplate?.TemplateName == FromGroupTemplateNames.PopupFormGroupTemplate)
                        AddFormGroupPopup(formGroupSetting);
                    else
                        throw new ArgumentException($"{nameof(formGroupSetting.FormGroupTemplate)}: CE249F5F-5645-4B74-BA17-5E1A9A5E73C8");
                }
                else if (setting is FormGroupArraySettingsDescriptor formGroupArraySetting)
                    AddFormGroupArray(formGroupArraySetting);
                else
                    throw new ArgumentException($"{nameof(formGroupSetting.FormGroupTemplate)}: CC7AD9E6-1CA5-4B9D-B1DF-D28AF8D6D757");

                return objectDictionary;

                void AddFormGroupArray(FormGroupArraySettingsDescriptor formGroupArraySetting) => objectDictionary.Add
                (
                    setting.Field,
                    mapper.Map<IEnumerable<object>, IEnumerable<Dictionary<string, object>>>
                    (
                        (IEnumerable<object>)propertiesDictionary[GetFieldName(setting.Field)]
                    )
                    .Select
                    (
                        dictionary => dictionary.ToObjectDictionaryFromValidatableObjects(mapper, formGroupArraySetting.FieldSettings)
                    ).ToList()//Need an ICollection<Dictionary<string, object>>
                );

                void AddFormGroupPopup(FormGroupSettingsDescriptor formGroupSetting) => objectDictionary.Add
                (
                    setting.Field,
                    mapper.Map<Dictionary<string, object>>
                    (
                        propertiesDictionary[GetFieldName(formGroupSetting.Field)]
                    ).ToObjectDictionaryFromValidatableObjects(mapper, formGroupSetting.FieldSettings)
                );

                void AddFormGroupInline(FormGroupSettingsDescriptor formGroupSetting) => objectDictionary.Add
                (
                    setting.Field,
                    ToObjectDictionaryFromValidatableObjects
                    (
                        propertiesDictionary,
                        mapper,
                        formGroupSetting.FieldSettings,
                        GetFieldName(formGroupSetting.Field)
                    )
                );

                void AddMultiSelects() => objectDictionary.Add
                (
                    setting.Field,
                    mapper.Map<IEnumerable<object>, IEnumerable<Dictionary<string, object>>>
                    (
                        (IEnumerable<object>)propertiesDictionary[GetFieldName(setting.Field)]
                    )
                );

                void AddSingleValueField() => objectDictionary.Add
                (
                    setting.Field,
                    propertiesDictionary[GetFieldName(setting.Field)]
                );
            });

            string GetFieldName(string field)
                => string.IsNullOrEmpty(parentField)
                    ? field
                    : $"{parentField}.{field}";
        }

        private static Dictionary<string, object> ToObjectDictionaryFromEntity(this Dictionary<string, object> propertiesDictionary, IMapper mapper, List<FormItemSettingsDescriptor> fieldSettings)
        {
            if (propertiesDictionary.IsEmpty())//object must be null - no fields to update
                return new Dictionary<string, object>();

            return fieldSettings.Aggregate(new Dictionary<string, object>(), (objectDictionary, setting) =>
            {
                if (setting is MultiSelectFormControlSettingsDescriptor multiSelectFormControlSetting)
                    AddMultiSelects();
                else if (setting is FormControlSettingsDescriptor controlSetting)
                    AddSingleValueField();//must stay after MultiSelect because MultiSelect extends FormControl
                else if (setting is FormGroupSettingsDescriptor formGroupSetting)
                {
                    AddFormGroup(formGroupSetting);
                }
                else if (setting is FormGroupArraySettingsDescriptor formGroupArraySetting)
                    AddFormGroupArray(formGroupArraySetting);
                else
                    throw new ArgumentException($"{nameof(formGroupSetting.FormGroupTemplate)}: CC7AD9E6-1CA5-4B9D-B1DF-D28AF8D6D757");

                return objectDictionary;

                void AddFormGroupArray(FormGroupArraySettingsDescriptor formGroupArraySetting) => objectDictionary.Add
                (
                    setting.Field,
                    mapper.Map<IEnumerable<object>, IEnumerable<Dictionary<string, object>>>
                    (
                        (IEnumerable<object>)propertiesDictionary[setting.Field]
                    )
                    .Select
                    (
                        dictionary => dictionary.ToObjectDictionaryFromEntity(mapper, formGroupArraySetting.FieldSettings)
                    ).ToList()//Need an ICollection<Dictionary<string, object>>
                );

                void AddFormGroup(FormGroupSettingsDescriptor formGroupSetting) => objectDictionary.Add
                (
                    setting.Field,
                    mapper.Map<Dictionary<string, object>>
                    (
                        propertiesDictionary[setting.Field]
                    ).ToObjectDictionaryFromEntity(mapper, formGroupSetting.FieldSettings)
                );

                void AddMultiSelects() => objectDictionary.Add
                (
                    setting.Field,
                    mapper.Map<IEnumerable<object>, IEnumerable<Dictionary<string, object>>>
                    (
                        (IEnumerable<object>)propertiesDictionary[setting.Field]
                    )
                );

                void AddSingleValueField() => objectDictionary.Add
                (
                    setting.Field,
                    propertiesDictionary[setting.Field]
                );
            });
        }

        public static void UpdateValidatables(this IEnumerable<IValidatable> properties, object source, List<FormItemSettingsDescriptor> fieldSettings, IMapper mapper, string parentField = null)
        {
            IDictionary<string, object> existingValues = mapper.Map<Dictionary<string, object>>(source) ?? new Dictionary<string, object>();
            IDictionary<string, IValidatable> propertiesDictionary = properties.ToDictionary(p => p.Name);
            foreach (var setting in fieldSettings)
            {
                if (setting is MultiSelectFormControlSettingsDescriptor multiSelectFormControlSetting)
                {
                    if (existingValues.TryGetValue(multiSelectFormControlSetting.Field, out object @value) && @value != null)
                    {
                        propertiesDictionary[GetFieldName(multiSelectFormControlSetting.Field)].Value = Activator.CreateInstance
                        (
                            typeof(ObservableCollection<>).MakeGenericType
                            (
                                Type.GetType(multiSelectFormControlSetting.MultiSelectTemplate.ModelType)
                            ),
                            new object[] { @value }
                        );
                    }
                }
                else if (setting is FormControlSettingsDescriptor controlSetting)
                {//must stay after MultiSelect because MultiSelect extends FormControl
                    if (existingValues.TryGetValue(controlSetting.Field, out object @value) && @value != null)
                        propertiesDictionary[GetFieldName(controlSetting.Field)].Value = @value;
                }
                else if (setting is FormGroupSettingsDescriptor formGroupSetting)
                {
                    if (existingValues.TryGetValue(formGroupSetting.Field, out object @value) && @value != null)
                    {
                        if (formGroupSetting.FormGroupTemplate == null)
                            throw new ArgumentException($"{nameof(formGroupSetting.FormGroupTemplate)}: 74E0697E-B5EF-4939-B0B4-8B7E4AE5544B");

                        if (formGroupSetting.FormGroupTemplate.TemplateName == FromGroupTemplateNames.InlineFormGroupTemplate)
                            properties.UpdateValidatables(@value, formGroupSetting.FieldSettings, mapper, GetFieldName(formGroupSetting.Field));
                        else if (formGroupSetting.FormGroupTemplate.TemplateName == FromGroupTemplateNames.PopupFormGroupTemplate)
                            propertiesDictionary[GetFieldName(formGroupSetting.Field)].Value = @value;
                        else
                            throw new ArgumentException($"{nameof(formGroupSetting.FormGroupTemplate.TemplateName)}: 5504FE49-2766-4D7C-916D-8FC633477DB1");
                    }
                }
                else if (setting is FormGroupArraySettingsDescriptor formGroupArraySetting)
                {
                    if (existingValues.TryGetValue(formGroupArraySetting.Field, out object @value) && @value != null)
                    {
                        propertiesDictionary[GetFieldName(formGroupArraySetting.Field)].Value = Activator.CreateInstance
                        (
                            typeof(ObservableCollection<>).MakeGenericType
                            (
                                Type.GetType(formGroupArraySetting.ModelType)
                            ),
                            new object[] { @value }
                        );
                    }
                }
            }

            string GetFieldName(string field)
                => string.IsNullOrEmpty(parentField) 
                    ? field 
                    : $"{parentField}.{field}";
        }

        const string EntityState = "EntityState";
        public static void UpdateEntityStates(Dictionary<string, object> existing, Dictionary<string, object> current, List<FormItemSettingsDescriptor> fieldSettings)
        {
            if (current.IsEmpty())
            {
                current[EntityState] = LogicBuilder.Domain.EntityStateType.Deleted;
                return;
            }

            if (existing.IsEmpty())
            {
                current[EntityState] = LogicBuilder.Domain.EntityStateType.Added;
            }
            else
            {
                current[EntityState] = new DictionaryComparer().Equals(current, existing)
                    ? LogicBuilder.Domain.EntityStateType.Unchanged
                    : LogicBuilder.Domain.EntityStateType.Modified;
            }
            

            foreach (var setting in fieldSettings)
            {
                if (setting is FormGroupSettingsDescriptor formGroupSetting)
                {
                    existing.TryGetValue(setting.Field, out object existingObject);
                    UpdateEntityStates((Dictionary<string, object>)existingObject ?? new Dictionary<string, object>(), (Dictionary<string, object>)current[setting.Field], formGroupSetting.FieldSettings);
                }
                else if (setting is MultiSelectFormControlSettingsDescriptor multiSelectFormControlSettingsDescriptor)
                {
                    existing.TryGetValue(setting.Field, out object existingCollection);
                    ICollection<Dictionary<string, object>> existingList = (ICollection<Dictionary<string, object>>)existingCollection ?? new List<Dictionary<string, object>>();
                    ICollection<Dictionary<string, object>> currentList = (ICollection<Dictionary<string, object>>)current[setting.Field];

                    if (currentList.Any() == true)
                    {
                        foreach (var entry in currentList)
                        {
                            if (entry.ExistsInList(existingList, multiSelectFormControlSettingsDescriptor.KeyFields))
                                entry[EntityState] = LogicBuilder.Domain.EntityStateType.Unchanged;
                            else
                                entry[EntityState] = LogicBuilder.Domain.EntityStateType.Added;
                        }
                    }

                    if (existingList.Any() == true)
                    {
                        foreach (var entry in existingList)
                        {
                            if (!entry.ExistsInList(currentList, multiSelectFormControlSettingsDescriptor.KeyFields))
                            {
                                entry[EntityState] = LogicBuilder.Domain.EntityStateType.Deleted;
                                currentList.Add(entry);
                            }
                        }
                    }
                }
                else if (setting is FormGroupArraySettingsDescriptor formGroupArraySetting)
                {
                    existing.TryGetValue(setting.Field, out object existingCollection);
                    ICollection<Dictionary<string, object>> existingList = (ICollection<Dictionary<string, object>>)existingCollection ?? new List<Dictionary<string, object>>();
                    ICollection<Dictionary<string, object>> currentList = (ICollection<Dictionary<string, object>>)current[setting.Field];

                    if (currentList.Any() == true)
                    {
                        foreach (var entry in currentList)
                        {
                            Dictionary<string, object> existingEntry = entry.GetExistingEntry(existingList, formGroupArraySetting.KeyFields);
                            UpdateEntityStates
                            (
                                existingEntry ?? new Dictionary<string, object>(),
                                entry,
                                formGroupArraySetting.FieldSettings
                            );
                        }
                    }

                    if (existingList.Any() == true)
                    {
                        foreach (var entry in existingList)
                        {
                            if (!entry.ExistsInList(currentList, formGroupArraySetting.KeyFields))
                            {
                                entry[EntityState] = LogicBuilder.Domain.EntityStateType.Deleted;
                                currentList.Add(entry);
                            }
                        }
                    }
                }
            }
        }

        private static bool IsEmpty(this IDictionary<string, object> dictionary)
            => dictionary?.Any() != true;
    }
}
