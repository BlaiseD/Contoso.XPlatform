using System;

namespace Contoso.Forms.Configuration
{
    public class CommandButtonDescriptor
    {
        public int Id { get; set; }
        public string ShortString { get; set; }
        public string LongString { get; set; }
        public string Command { get; set; }
        public int? GridId { get; set; }
        public bool? GridCommandButton { get; set; }
        public bool? SelectFormButton { get; set; }
        public string ButtonIcon { get; set; }
    }
}
