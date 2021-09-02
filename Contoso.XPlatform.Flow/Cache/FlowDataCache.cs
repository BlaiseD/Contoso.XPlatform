using System.Collections.Generic;

namespace Contoso.XPlatform.Flow.Cache
{
    public class FlowDataCache
    {
        public Dictionary<string, object> Items { get; set; } = new Dictionary<string, object>();
    }
}
