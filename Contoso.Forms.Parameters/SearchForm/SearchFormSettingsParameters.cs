using Contoso.Forms.Parameters.Bindings;
using Contoso.Parameters.Expansions;
using LogicBuilder.Attributes;
using System;

namespace Contoso.Forms.Parameters.SearchForm
{
    public class SearchFormSettingsParameters
    {
		public SearchFormSettingsParameters
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
			string filterPlaceholder,

			[Comments("")]
			CollectionViewItemBindingsDictionaryParameters bindings,

			[Comments("")]
			SortCollectionParameters sortCollection,

			[Comments("")]
			SearchFilterGroupParameters searchFilterGroup,

			[Comments("")]
			RequestDetailsParameters requestDetails
		)
		{
			Title = title;
			ModelType = modelType;
			LoadingIndicatorText = loadingIndicatorText;
			ItemTemplateName = itemTemplateName;
			FilterPlaceholder = filterPlaceholder;
			Bindings = bindings;
			SortCollection = sortCollection;
			SearchFilterGroup = searchFilterGroup;
			RequestDetails = requestDetails;
		}

		public string Title { get; set; }
		public Type ModelType { get; set; }
		public string LoadingIndicatorText { get; set; }
		public string ItemTemplateName { get; set; }
		public string FilterPlaceholder { get; set; }
		public CollectionViewItemBindingsDictionaryParameters Bindings { get; set; }
		public SortCollectionParameters SortCollection { get; set; }
		public SearchFilterGroupParameters SearchFilterGroup { get; set; }
		public RequestDetailsParameters RequestDetails { get; set; }
    }
}