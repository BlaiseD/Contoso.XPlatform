using Contoso.Bsl.Flow.Requests;
using Contoso.Bsl.Flow.Responses;
using System.Collections.Generic;

namespace Contoso.Bsl.Flow.Cache
{
    public class FlowDataCache
    {
        public BaseRequest Request { get; set; }
        public BaseResponse Response { get; set; }
        public Dictionary<string, object> Items { get; set; } = new Dictionary<string, object>();
    }
}
