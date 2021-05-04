using System.Collections.Generic;

namespace Contoso.Forms.Parameters.Navigation
{
    public class NavigationMenuItemParameters
    {
        public NavigationMenuItemParameters(string initialModule = "initial", string text = "menuText", string icon = "menuText", List<NavigationMenuItemParameters> SubItems = null)
        {
            this.InitialModule = initialModule;
            this.Text = text;
            this.Icon = icon;
            this.SubItems = SubItems;
        }

        public NavigationMenuItemParameters()
        {
        }

        public string InitialModule { get; set; }
        public string Text { get; set; }
        public string Icon { get; set; }
        public bool Active { get; set; }
        public List<NavigationMenuItemParameters> SubItems { get; set; }
    }
}
