using System.Collections.Generic;

namespace Contoso.Bsl.Business.Responses
{
    public class GetDropDownListResponse
    {
        public IEnumerable<dynamic> DropDownList { get; set; }
    }
}
