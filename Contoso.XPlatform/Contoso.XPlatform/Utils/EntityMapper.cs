using AutoMapper;
using Contoso.Forms.Configuration.EditForm;
using Contoso.XPlatform.ViewModels.Validatables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contoso.XPlatform.Utils
{
    public static class EntityMapper
    {
        internal static IDictionary<string, object> ToObjectDictionary(this IEnumerable<IValidatable> properties)
        {
            return properties.Aggregate(new Dictionary<string, object>(), (dictionary, next) =>
            {
                string[] nameParts = next.Name.Split('.', StringSplitOptions.RemoveEmptyEntries);

                if (nameParts.Length == 1)
                {
                    dictionary.Add(nameParts[0], next.Value);
                }
                else
                {
                    if (!dictionary.TryGetValue(nameParts[0], out object value))
                        dictionary.Add(nameParts[0], new Dictionary<string, object>());

                    for (int i = 1; i < nameParts.Length; i++)
                    {
                        var parentDictionary = (IDictionary<string, object>)dictionary[nameParts[i - 1]];
                        if (!parentDictionary.ContainsKey(nameParts[i]))
                        {
                            if (i == nameParts.Length - 1)
                                parentDictionary.Add(nameParts[i], next.Value);
                            else
                                parentDictionary.Add(nameParts[i], new Dictionary<string, object>());
                        }
                    }
                }

                return dictionary;
            });
        }

        public static void UpdateValidatables(this IEnumerable<IValidatable> properties, List<FormItemSettingsDescriptor> fieldSettings, IDictionary<string, object> existingValues, IMapper mapper, string parentField = null)
        {
            IDictionary<string, IValidatable> propertiesDictionary = properties.ToDictionary(p => p.Name);
            foreach (var setting in fieldSettings)
            {
                if (setting is FormControlSettingsDescriptor controlSetting)
                {
                    if (existingValues.TryGetValue(controlSetting.Field, out object @value) && @value != null)
                        propertiesDictionary[GetFieldName(controlSetting.Field)].Value = @value;
                }
                else if (setting is FormGroupSettingsDescriptor formGroupSetting)
                {
                    if (existingValues.TryGetValue(formGroupSetting.Field, out object @value) && @value != null)
                    {
                        if (formGroupSetting.FormGroupTemplate?.TemplateName == FromGroupTemplateNames.InlineFormGroupTemplate)
                        {
                            Dictionary<string, object> entity = mapper.Map<Dictionary<string, object>>(@value);
                            properties.UpdateValidatables(formGroupSetting.FieldSettings, entity, mapper, GetFieldName(formGroupSetting.Field));
                        }
                    }
                }
            }

            string GetFieldName(string field)
                => string.IsNullOrEmpty(parentField) 
                    ? field 
                    : $"{parentField}.{field}";
        }
    }
}
