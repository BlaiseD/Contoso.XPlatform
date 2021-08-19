using Contoso.Common.Configuration.ExpansionDescriptors;
using Contoso.Forms.Configuration.Bindings;

namespace Contoso.Forms.Configuration.SearchForm
{
    public class SearchFormSettingsDescriptor
    {
        public string Title { get; set; }
        public string ModelType { get; set; }
        public string DataType { get; set; }
        public string LoadingIndicatorText { get; set; }
        public string ItemTemplateName { get; set; }
        public CollectionViewItemBindingsDictionary Bindings { get; set; }
        public SortCollectionDescriptor SortDescriptor { get; set; }
        public SearchFilterGroupDescriptor SearchDescriptor { get; set; }
        public RequestDetailsDescriptor RequestDetails { get; set; }
    }
}
