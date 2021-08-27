namespace Contoso.Forms.Parameters
{
    public class RequestDetailsParameters
    {
		public RequestDetailsParameters(string modelType, string dataType, string modelReturnType, string dataReturnType, string dataSourceUrl, string getUrl, string addUrl, string updateUrl, string deleteUrl)
		{
			ModelType = modelType;
			DataType = dataType;
			ModelReturnType = modelReturnType;
			DataReturnType = dataReturnType;
			DataSourceUrl = dataSourceUrl;
			GetUrl = getUrl;
			AddUrl = addUrl;
			UpdateUrl = updateUrl;
			DeleteUrl = deleteUrl;
		}

		public string ModelType { get; set; }
		public string DataType { get; set; }
		public string ModelReturnType { get; set; }
		public string DataReturnType { get; set; }
		public string DataSourceUrl { get; set; }
		public string GetUrl { get; set; }
		public string AddUrl { get; set; }
		public string UpdateUrl { get; set; }
		public string DeleteUrl { get; set; }
    }
}