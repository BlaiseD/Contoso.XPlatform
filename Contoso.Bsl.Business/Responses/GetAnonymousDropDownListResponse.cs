using System.Collections.Generic;

namespace Contoso.Bsl.Business.Responses
{
    public class GetAnonymousDropDownListResponse : BaseResponse
    {
        public IEnumerable<object> DropDownList { get; set; }
    }
}
