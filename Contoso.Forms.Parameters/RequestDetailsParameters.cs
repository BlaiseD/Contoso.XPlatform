using LogicBuilder.Attributes;
using System;

namespace Contoso.Forms.Parameters
{
    public class RequestDetailsParameters
    {
		public RequestDetailsParameters
		(
			[Comments("")]
			Type modelType,

			[Comments("")]
			Type dataType,

			[Comments("")]
			Type modelReturnType,

			[Comments("")]
			Type dataReturnType,

			[Comments("")]
			string dataSourceUrl,

			[Comments("")]
			string getUrl,

			[Comments("")]
			string addUrl,

			[Comments("")]
			string updateUrl,

			[Comments("")]
			string deleteUrl
		)
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

		public Type ModelType { get; set; }
		public Type DataType { get; set; }
		public Type ModelReturnType { get; set; }
		public Type DataReturnType { get; set; }
		public string DataSourceUrl { get; set; }
		public string GetUrl { get; set; }
		public string AddUrl { get; set; }
		public string UpdateUrl { get; set; }
		public string DeleteUrl { get; set; }
    }
}