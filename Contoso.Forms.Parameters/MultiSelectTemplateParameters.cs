using Contoso.Parameters.Expressions;

namespace Contoso.Forms.Parameters
{
    public class MultiSelectTemplateParameters
    {
		public MultiSelectTemplateParameters(string templateName, string placeHolderText, string textField, string valueField, string modelType, string loadingIndicatorText, SelectorLambdaOperatorParameters textAndValueSelector, RequestDetailsParameters requestDetails)
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
		public string ModelType { get; set; }
		public string LoadingIndicatorText { get; set; }
		public SelectorLambdaOperatorParameters TextAndValueSelector { get; set; }
		public RequestDetailsParameters RequestDetails { get; set; }
    }
}