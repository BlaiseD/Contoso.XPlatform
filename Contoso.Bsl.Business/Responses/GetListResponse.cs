using Contoso.Domain;
using System.Collections.Generic;

namespace Contoso.Bsl.Business.Responses
{
    public class GetListResponse
    {
        public IEnumerable<ViewModelBase> List { get; set; }
    }
}
