namespace Contoso.Forms.Parameters.SearchForm
{
    public class SearchFilterParameters : SearchFilterParametersBase
    {
		public SearchFilterParameters(string field)
		{
			Field = field;
		}

		public string Field { get; set; }
    }
}