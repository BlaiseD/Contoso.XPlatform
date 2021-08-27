using Contoso.Parameters.Expansions;
using Contoso.Parameters.Expressions;

namespace Contoso.Forms.Parameters.EditForm
{
    public class EditFormRequestDetailsParameters
    {
		public EditFormRequestDetailsParameters(string getUrl, string addUrl, string updateUrl, string deleteUrl, string modelType, string dataType, EditType editType, FilterLambdaOperatorParameters filter, SelectExpandDefinitionParameters selectExpandDefinition)
		{
			GetUrl = getUrl;
			AddUrl = addUrl;
			UpdateUrl = updateUrl;
			DeleteUrl = deleteUrl;
			ModelType = modelType;
			DataType = dataType;
			EditType = editType;
			Filter = filter;
			SelectExpandDefinition = selectExpandDefinition;
		}

		public string GetUrl { get; set; }
		public string AddUrl { get; set; }
		public string UpdateUrl { get; set; }
		public string DeleteUrl { get; set; }
		public string ModelType { get; set; }
		public string DataType { get; set; }
		public EditType EditType { get; set; }
		public FilterLambdaOperatorParameters Filter { get; set; }
		public SelectExpandDefinitionParameters SelectExpandDefinition { get; set; }
    }
}