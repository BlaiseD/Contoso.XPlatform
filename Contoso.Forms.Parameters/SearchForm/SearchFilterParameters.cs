using LogicBuilder.Attributes;

namespace Contoso.Forms.Parameters.SearchForm
{
    public class SearchFilterParameters : SearchFilterParametersBase
    {
		public SearchFilterParameters
		(
			[Comments("")]
			string field
		)
		{
			Field = field;
		}

		public string Field { get; set; }
    }
}