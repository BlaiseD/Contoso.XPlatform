using Contoso.Domain.Entities;
using System.Collections.Generic;

namespace Contoso.Bsl.Business.Responses
{
    public class GetLookupDropDownListResponse
    {
        public IEnumerable<LookUpsModel> DropDownList { get; set; }
    }
}
