using Contoso.Forms.Parameters.Bindings;
using Contoso.Parameters.Expansions;

namespace Contoso.Forms.Parameters.SearchForm
{
    public class SearchFormSettingsParameters
    {
		public SearchFormSettingsParameters(string title, string modelType, string loadingIndicatorText, string itemTemplateName, string filterPlaceholder, CollectionViewItemBindingsDictionaryParameters bindings, SortCollectionParameters sortCollection, SearchFilterGroupParameters searchFilterGroup, RequestDetailsParameters requestDetails)
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
		public string ModelType { get; set; }
		public string LoadingIndicatorText { get; set; }
		public string ItemTemplateName { get; set; }
		public string FilterPlaceholder { get; set; }
		public CollectionViewItemBindingsDictionaryParameters Bindings { get; set; }
		public SortCollectionParameters SortCollection { get; set; }
		public SearchFilterGroupParameters SearchFilterGroup { get; set; }
		public RequestDetailsParameters RequestDetails { get; set; }
    }
}