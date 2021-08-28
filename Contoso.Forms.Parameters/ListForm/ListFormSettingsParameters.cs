using Contoso.Forms.Parameters.Bindings;
using Contoso.Parameters.Expressions;
using LogicBuilder.Attributes;
using System;

namespace Contoso.Forms.Parameters.ListForm
{
    public class ListFormSettingsParameters
    {
		public ListFormSettingsParameters
		(
			[Comments("")]
			string title,

			[Comments("")]
			Type modelType,

			[Comments("")]
			string loadingIndicatorText,

			[Comments("")]
			string itemTemplateName,

			[Comments("")]
			CollectionViewItemBindingsDictionaryParameters bindings,

			[Comments("")]
			SelectorLambdaOperatorParameters fieldsSelector,

			[Comments("")]
			RequestDetailsParameters requestDetails
		)
		{
			Title = title;
			ModelType = modelType;
			LoadingIndicatorText = loadingIndicatorText;
			ItemTemplateName = itemTemplateName;
			Bindings = bindings;
			FieldsSelector = fieldsSelector;
			RequestDetails = requestDetails;
		}

		public string Title { get; set; }
		public Type ModelType { get; set; }
		public string LoadingIndicatorText { get; set; }
		public string ItemTemplateName { get; set; }
		public CollectionViewItemBindingsDictionaryParameters Bindings { get; set; }
		public SelectorLambdaOperatorParameters FieldsSelector { get; set; }
		public RequestDetailsParameters RequestDetails { get; set; }
    }
}