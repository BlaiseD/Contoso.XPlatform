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
			string dataSourceUrl
		)
		{
			ModelType = modelType;
			DataType = dataType;
			ModelReturnType = modelReturnType;
			DataReturnType = dataReturnType;
			DataSourceUrl = dataSourceUrl;
		}

		public Type ModelType { get; set; }
		public Type DataType { get; set; }
		public Type ModelReturnType { get; set; }
		public Type DataReturnType { get; set; }
		public string DataSourceUrl { get; set; }
    }
}