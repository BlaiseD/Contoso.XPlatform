using Contoso.Parameters.Expressions;

namespace Contoso.Forms.Parameters
{
    public class DropDownTemplateParameters
    {
		public DropDownTemplateParameters(string templateName, string placeHolderText, string textField, string valueField, string loadingIndicatorText, SelectorLambdaOperatorParameters textAndValueSelector, RequestDetailsParameters requestDetails)
		{
			TemplateName = templateName;
			PlaceHolderText = placeHolderText;
			TextField = textField;
			ValueField = valueField;
			LoadingIndicatorText = loadingIndicatorText;
			TextAndValueSelector = textAndValueSelector;
			RequestDetails = requestDetails;
		}

		public string TemplateName { get; set; }
		public string PlaceHolderText { get; set; }
		public string TextField { get; set; }
		public string ValueField { get; set; }
		public string LoadingIndicatorText { get; set; }
		public SelectorLambdaOperatorParameters TextAndValueSelector { get; set; }
		public RequestDetailsParameters RequestDetails { get; set; }
    }
}