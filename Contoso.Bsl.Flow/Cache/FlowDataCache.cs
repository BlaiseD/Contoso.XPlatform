using Contoso.Bsl.Flow.Requests;
using Contoso.Bsl.Flow.Responses;
using Contoso.XPlatform.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contoso.Bsl.Flow.Cache
{
    public class FlowDataCache
    {
        public BaseRequest Request { get; set; }
        public BaseResponse Response { get; set; }
        public ModelDictionary ModelItems { get; set; } = new ModelDictionary();
        public Dictionary<string, object> Items { get; set; } = new Dictionary<string, object>();
    }
}
