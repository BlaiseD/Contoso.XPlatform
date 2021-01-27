using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.Parameters.Expansions
{
    public class SelectExpandDefinitionParameters
    {
        public SelectExpandDefinitionParameters()
        {
        }

        public SelectExpandDefinitionParameters(List<string> selects, List<SelectExpandItemParameters> expandedItems)
        {
            Selects = selects;
            ExpandedItems = expandedItems;
        }

        public List<string> Selects { get; set; } = new List<string>();
        public List<SelectExpandItemParameters> ExpandedItems { get; set; } = new List<SelectExpandItemParameters>();
    }
}
