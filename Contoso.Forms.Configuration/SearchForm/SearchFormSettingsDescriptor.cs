using Contoso.Common.Configuration.ExpansionDescriptors;
using Contoso.Forms.Configuration.Bindings;
using Contoso.Forms.Configuration.ItemFilter;
using System.Collections.Generic;

namespace Contoso.Forms.Configuration.SearchForm
{
    public class SearchFormSettingsDescriptor
    {
        public string Title { get; set; }
        public string ModelType { get; set; }
        public string LoadingIndicatorText { get; set; }
        public string ItemTemplateName { get; set; }
        public string FilterPlaceholder { get; set; }
        public Dictionary<string, CollectionViewItemBindingDescriptor> Bindings { get; set; }
        public SortCollectionDescriptor SortCollection { get; set; }
        public SearchFilterGroupDescriptor SearchFilterGroup { get; set; }
        public ItemFilterGroupDescriptor ItemFilterGroup { get; set; }
        public RequestDetailsDescriptor RequestDetails { get; set; }
    }
}
