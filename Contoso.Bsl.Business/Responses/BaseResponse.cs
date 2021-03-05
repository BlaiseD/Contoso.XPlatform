using System.Collections.Generic;

namespace Contoso.Bsl.Business.Responses
{
    public abstract class BaseResponse
    {
        public bool Success { get; set; }
        public ICollection<string> ErrorMessages { get; set; }
    }
}
