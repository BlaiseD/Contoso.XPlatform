﻿using Contoso.Parameters.Expansions;
using Contoso.Parameters.Expressions;
using LogicBuilder.Attributes;
using System;

namespace Contoso.Forms.Parameters.EditForm
{
    public class EditFormRequestDetailsParameters
    {
		public EditFormRequestDetailsParameters
		(
			[Comments("API end point to get the entity.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "api/Entity/GetEntity")]
			string getUrl,

			[Comments("API end point to add the entity.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "api/Student/Save")]
			string addUrl,

			[Comments("API end point to update the entity.")]
			[NameValue(AttributeNames.DEFAULTVALUE, "api/Student/Save")]
			string updateUrl,

			[Comments("The model type. Click the function button and use the configured GetType function.  Use the Assembly qualified type name for the type argument (For literals, the full name (e.g. System.Int32) is sufficient.)")]
			Type modelType,

			[Comments("The data type. Click the function button and use the configured GetType function.  Use the Assembly qualified type name for the type argument (For literals, the full name (e.g. System.Int32) is sufficient.)")]
			Type dataType,

			[Comments("Defines the filter for the single object being edited - only applicable when the edit type is update.")]
			FilterLambdaOperatorParameters filter,

			[Comments("Defines and navigation properties to include in the edit model")]
			SelectExpandDefinitionParameters selectExpandDefinition
		)
		{
			GetUrl = getUrl;
			AddUrl = addUrl;
			UpdateUrl = updateUrl;
			ModelType = modelType;
			DataType = dataType;
			Filter = filter;
			SelectExpandDefinition = selectExpandDefinition;
		}

		public string GetUrl { get; set; }
		public string AddUrl { get; set; }
		public string UpdateUrl { get; set; }
		public Type ModelType { get; set; }
		public Type DataType { get; set; }
		public FilterLambdaOperatorParameters Filter { get; set; }
		public SelectExpandDefinitionParameters SelectExpandDefinition { get; set; }
    }
}