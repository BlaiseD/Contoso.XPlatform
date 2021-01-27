using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.Parameters.Expansions
{
    public class SelectExpandItemParameters
    {
        public SelectExpandItemParameters()
        {
        }

        public SelectExpandItemParameters(string memberName, SelectExpandItemFilterParameters filter, SelectExpandItemQueryFunctionParameters queryFunction, List<string> selects, List<SelectExpandItemParameters> expandedItems)
        {
            MemberName = memberName;
            Filter = filter;
            QueryFunction = queryFunction;
            Selects = selects;
            ExpandedItems = expandedItems;
        }

        public string MemberName { get; set; }
        public SelectExpandItemFilterParameters Filter { get; set; }
        public SelectExpandItemQueryFunctionParameters QueryFunction { get; set; }
        public List<string> Selects { get; set; } = new List<string>();
        public List<SelectExpandItemParameters> ExpandedItems { get; set; } = new List<SelectExpandItemParameters>();
    }
}
