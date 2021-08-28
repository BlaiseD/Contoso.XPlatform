using LogicBuilder.Attributes;
using System.Collections.Generic;

namespace Contoso.Forms.Parameters.SearchForm
{
    public class SearchFilterGroupParameters : SearchFilterParametersBase
    {
		public SearchFilterGroupParameters
		(
			[Comments("")]
			ICollection<SearchFilterParametersBase> filters
		)
		{
			Filters = filters;
		}

		public ICollection<SearchFilterParametersBase> Filters { get; set; }
    }
}