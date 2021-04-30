using System;

namespace Contoso.Forms.Configuration
{
    public class CommandButtonDescriptor
    {
        public int Id { get; set; }
        public string ShortString { get; set; }
        public string LongString { get; set; }
        public bool Submit { get; set; }
        public int? GridId { get; set; }
        public bool? GridCommandButton { get; set; }
        public bool? SelectFormButton { get; set; }
        public string ButtonIcon { get; set; }
    }
}
