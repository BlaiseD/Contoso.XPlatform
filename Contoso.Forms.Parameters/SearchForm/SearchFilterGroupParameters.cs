using System.Collections.Generic;

namespace Contoso.Forms.Parameters.SearchForm
{
    public class SearchFilterGroupParameters : SearchFilterParametersBase
    {
		public SearchFilterGroupParameters(ICollection<SearchFilterParametersBase> filters)
		{
			Filters = filters;
		}

		public ICollection<SearchFilterParametersBase> Filters { get; set; }
    }
}