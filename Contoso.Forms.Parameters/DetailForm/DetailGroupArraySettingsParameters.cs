using Contoso.Forms.Parameters.EditForm;
using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;

namespace Contoso.Forms.Parameters.DetailForm
{
    public class DetailGroupArraySettingsParameters : DetailItemSettingsParameters
    {
		public DetailGroupArraySettingsParameters
		(

			[ParameterEditorControl(ParameterControlType.ParameterSourcedPropertyInput)]
			[NameValue(AttributeNames.PROPERTYSOURCEPARAMETER, "fieldTypeSource")]
			[Comments("Update fieldTypeSource first. Source property name from the target object.")]
			string field,

			[NameValue(AttributeNames.DEFAULTVALUE, "Title")]
			[Comments("Title for the form group.")]
			string title,

			[Comments("e.g. T. The element type for the list being dispalyed. Click the function button and use the configured GetType function.  Use the Assembly qualified type name for the type argument.")]
			Type modelType,

			[Comments("e.g. ICollection<T>. The type for the list being dispalyed. Click the function button and use the configured GetType function.  Use the Assembly qualified type name for the type argument.")]
			Type type,

			[Comments("Template and property bindings to be displayed for each item in the list.")]
			FormsCollectionDisplayTemplateParameters formsCollectionDisplayTemplate,

			[Comments("XAML template for the form group.")]
			FormGroupTemplateParameters formGroupTemplate,

			[Comments("Configuration for each field in one of the array's form groups.")]
			List<DetailItemSettingsParameters> fieldSettings,

			[ParameterEditorControl(ParameterControlType.ParameterSourceOnly)]
			[Comments("Fully qualified class name for the model type.")]
			string fieldTypeSource = "Contoso.Domain.Entities"
		) : base(field)
		{
			Title = title;
			ModelType = modelType;
			Type = type;
			FormsCollectionDisplayTemplate = formsCollectionDisplayTemplate;
			FormGroupTemplate = formGroupTemplate;
			FieldSettings = fieldSettings;
		}

		public string Title { get; set; }
		public Type ModelType { get; set; }
		public Type Type { get; set; }
		public FormsCollectionDisplayTemplateParameters FormsCollectionDisplayTemplate { get; set; }
		public FormGroupTemplateParameters FormGroupTemplate { get; set; }
		public List<DetailItemSettingsParameters> FieldSettings { get; set; }
    }
}