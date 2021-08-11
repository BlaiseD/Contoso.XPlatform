﻿using Contoso.Common.Configuration.ExpansionDescriptors;
using Contoso.Common.Configuration.ExpressionDescriptors;

namespace Contoso.Forms.Configuration.EditForm
{
    public class EditFormRequestDetailsDescriptor
    {
        public string GetUrl { get; set; }
        public string AddUrl { get; set; }
        public string UpdateUrl { get; set; }
        public string DeleteUrl { get; set; }
        public string ModelType { get; set; }
        public string DataType { get; set; }
        public EditType EditType { get; set; }
        public FilterLambdaOperatorDescriptor Filter { get; set; }
        public SelectExpandDefinitionDescriptor SelectExpandDefinition { get; set; }
    }
}
