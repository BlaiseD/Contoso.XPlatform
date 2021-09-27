using Contoso.Domain;
using System.Collections.Generic;

namespace Contoso.Bsl.Business.Responses
{
    public class GetObjectDropDownListResponse : BaseResponse
    {
        public IEnumerable<EntityModelBase> DropDownList { get; set; }
    }
}
