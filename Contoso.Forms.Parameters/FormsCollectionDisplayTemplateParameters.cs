using Contoso.Forms.Parameters.Bindings;
using Contoso.Parameters.Expressions;
using LogicBuilder.Attributes;
using System;

namespace Contoso.Forms.Parameters
{
    public class FormsCollectionDisplayTemplateParameters
    {
		public FormsCollectionDisplayTemplateParameters
		(
			[Comments("")]
			string templateName,

			[Comments("")]
			string placeHolderText,

			[Comments("")]
			Type modelType,

			[Comments("")]
			string loadingIndicatorText,

			[Comments("")]
			CollectionViewItemBindingsDictionaryParameters bindings,

			[Comments("")]
			SelectorLambdaOperatorParameters collectionSelector,

			[Comments("")]
			RequestDetailsParameters requestDetails
		)
		{
			TemplateName = templateName;
			PlaceHolderText = placeHolderText;
			ModelType = modelType;
			LoadingIndicatorText = loadingIndicatorText;
			Bindings = bindings;
			CollectionSelector = collectionSelector;
			RequestDetails = requestDetails;
		}

		public string TemplateName { get; set; }
		public string PlaceHolderText { get; set; }
		public Type ModelType { get; set; }
		public string LoadingIndicatorText { get; set; }
		public CollectionViewItemBindingsDictionaryParameters Bindings { get; set; }
		public SelectorLambdaOperatorParameters CollectionSelector { get; set; }
		public RequestDetailsParameters RequestDetails { get; set; }
    }
}