using Contoso.Parameters.Expressions;
using LogicBuilder.Attributes;
using System;

namespace Contoso.Forms.Parameters
{
    public class MultiSelectTemplateParameters
    {
		public MultiSelectTemplateParameters
		(
			[Comments("")]
			string templateName,

			[Comments("")]
			string placeHolderText,

			[Comments("")]
			string textField,

			[Comments("")]
			string valueField,

			[Comments("")]
			Type modelType,

			[Comments("")]
			string loadingIndicatorText,

			[Comments("")]
			SelectorLambdaOperatorParameters textAndValueSelector,

			[Comments("")]
			RequestDetailsParameters requestDetails
		)
		{
			TemplateName = templateName;
			PlaceHolderText = placeHolderText;
			TextField = textField;
			ValueField = valueField;
			ModelType = modelType;
			LoadingIndicatorText = loadingIndicatorText;
			TextAndValueSelector = textAndValueSelector;
			RequestDetails = requestDetails;
		}

		public string TemplateName { get; set; }
		public string PlaceHolderText { get; set; }
		public string TextField { get; set; }
		public string ValueField { get; set; }
		public Type ModelType { get; set; }
		public string LoadingIndicatorText { get; set; }
		public SelectorLambdaOperatorParameters TextAndValueSelector { get; set; }
		public RequestDetailsParameters RequestDetails { get; set; }
    }
}