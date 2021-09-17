using System.Collections.Generic;

namespace Contoso.Forms.Configuration.EditForm
{
    public class HeaderBindingsDescriptor
    {
        public string HeaderStringFormat { get; set; }
        public string SubTitleStringFormat { get; set; }
        public List<string> Fields { get; set; }
    }
}
