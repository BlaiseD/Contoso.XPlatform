using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.Parameters.Expressions
{
    public class MemberBindingItem
    {
        public MemberBindingItem
        (
            [Comments("Property name.")]
            [NameValue(AttributeNames.USEFOREQUALITY, "true")]
            [NameValue(AttributeNames.USEFORHASHCODE, "true")]
            string property,

            [Comments("Selector.")]
            IExpressionParameter selector
        )
        {
            Property = property;
            Selector = selector;
        }

        public string Property { get; set; }
        public IExpressionParameter Selector { get; set; }
    }
}
