using System.Collections.Generic;
using System;

namespace Contoso.Bsl.Configuration.ExpressionDescriptors
{
    public class CollectionConstantOperatorDescriptor : IExpressionDescriptor
    {
		public string ElementType { get; set; }
		public ICollection<object> ConstantValues { get; set; }
    }
}