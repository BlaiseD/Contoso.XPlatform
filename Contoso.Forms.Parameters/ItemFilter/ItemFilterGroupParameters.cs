using LogicBuilder.Attributes;
using System.Collections.Generic;

namespace Contoso.Forms.Parameters.ItemFilter
{
    public class ItemFilterGroupParameters : ItemFilterParametersBase
    {
		public ItemFilterGroupParameters
		(
			[Comments("")]
			string logic,

			[Comments("")]
			ICollection<ItemFilterParametersBase> filters
		)
		{
			Logic = logic;
			Filters = filters;
		}

		public string Logic { get; set; }
		public ICollection<ItemFilterParametersBase> Filters { get; set; }
    }
}