using Contoso.Forms.Parameters.Bindings;
using Contoso.Parameters.Expressions;
using LogicBuilder.Attributes;
using System;

namespace Contoso.Forms.Parameters
{
    public class FormsCollectionDisplayTemplateParameters
    {
		public FormsCollectionDisplayTemplateParameters
		(
			[Comments("")]
			string templateName,

			[Comments("")]
			CollectionViewItemBindingsDictionaryParameters bindings
		)
		{
			TemplateName = templateName;
			Bindings = bindings;
		}

		public string TemplateName { get; set; }
		public CollectionViewItemBindingsDictionaryParameters Bindings { get; set; }
    }
}