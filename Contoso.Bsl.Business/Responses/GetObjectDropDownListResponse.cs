using Contoso.Domain;
using System.Collections.Generic;

namespace Contoso.Bsl.Business.Responses
{
    public class GetObjectDropDownListResponse
    {
        public IEnumerable<ViewModelBase> DropDownList { get; set; }
    }
}
