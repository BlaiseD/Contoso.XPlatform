using System.Collections.Generic;
using System;

namespace Contoso.Parameters.Expressions
{
    public class CollectionConstantOperatorParameter : IExpressionParameter
    {
		public CollectionConstantOperatorParameter()
		{
		}

		public CollectionConstantOperatorParameter(ICollection<object> constantValues, Type elementType)
		{
			ConstantValues = constantValues;
			ElementType = elementType;
		}

		public Type ElementType { get; set; }
		public ICollection<object> ConstantValues { get; set; }
    }
}