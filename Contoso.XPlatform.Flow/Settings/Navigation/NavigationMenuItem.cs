using System.Collections.Generic;

namespace Contoso.XPlatform.Flow.Settings.Navigation
{
    public class NavigationMenuItem
    {
        public NavigationMenuItem(string initialModule = "initial", string Text = "menuText", List<NavigationMenuItem> SubItems = null)
        {
            this.InitialModule = initialModule;
            this.Text = Text;
            this.SubItems = SubItems;
        }

        public NavigationMenuItem()
        {
        }

        public string InitialModule { get; set; }
        public string Text { get; set; }
        public bool Active { get; set; }
        public List<NavigationMenuItem> SubItems { get; set; }
    }
}
