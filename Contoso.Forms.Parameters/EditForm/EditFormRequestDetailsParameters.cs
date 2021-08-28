using Contoso.Parameters.Expansions;
using Contoso.Parameters.Expressions;
using LogicBuilder.Attributes;
using System;

namespace Contoso.Forms.Parameters.EditForm
{
    public class EditFormRequestDetailsParameters
    {
		public EditFormRequestDetailsParameters
		(
			[Comments("")]
			string getUrl,

			[Comments("")]
			string addUrl,

			[Comments("")]
			string updateUrl,

			[Comments("")]
			string deleteUrl,

			[Comments("")]
			Type modelType,

			[Comments("")]
			Type dataType,

			[Comments("")]
			EditType editType,

			[Comments("")]
			FilterLambdaOperatorParameters filter,

			[Comments("")]
			SelectExpandDefinitionParameters selectExpandDefinition
		)
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
		public Type ModelType { get; set; }
		public Type DataType { get; set; }
		public EditType EditType { get; set; }
		public FilterLambdaOperatorParameters Filter { get; set; }
		public SelectExpandDefinitionParameters SelectExpandDefinition { get; set; }
    }
}