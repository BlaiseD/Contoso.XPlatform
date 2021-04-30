using LogicBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.XPlatform.Flow.Settings.Navigation
{
    public class NavigationBar
    {
        public NavigationBar
        (
            [Comments("Brand text for the navigation bar.")]
            string brandText = "Contoso",

            [Comments("Current module indicator used to determine which menu item gets set to active.")]
            string currentModule = "initial",

            [Comments("True if the grid is sortable otherwise false")]
            List<NavigationMenuItem> MenuItems = null
        )
        {
            this.BrandText = brandText;
            this.CurrentModule = currentModule;
            this.MenuItems = MenuItems ?? new List<NavigationMenuItem>();
        }

        public NavigationBar()
        {
        }

        public string BrandText { get; set; }
        public string CurrentModule { get; set; }
        public List<NavigationMenuItem> MenuItems { get; set; }
    }
}
