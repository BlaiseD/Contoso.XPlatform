using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contoso.Bsl.Flow.Flow
{
    public static class GenericsHelpers<T>
    {
        [AlsoKnownAs("ToList")]
        [FunctionGroup(FunctionGroup.Standard)]
        public static List<T> ToList(IEnumerable<T> enumerable)
        {
            return enumerable.ToList();
        }

        [AlsoKnownAs("Single")]
        [FunctionGroup(FunctionGroup.Standard)]
        public static T Single(IEnumerable<T> enumerable)
        {
            return enumerable.Single();
        }

        [AlsoKnownAs("SingleOrDefault")]
        [FunctionGroup(FunctionGroup.Standard)]
        public static T SingleOrDefault(IEnumerable<T> enumerable)
        {
            return enumerable.SingleOrDefault();
        }

        [AlsoKnownAs("ItemByIndex")]
        [FunctionGroup(FunctionGroup.Standard)]
        public static T ItemByIndex(IEnumerable<T> enumerable, int index)
        {
            return enumerable.ToList()[index];
        }
    }
}
