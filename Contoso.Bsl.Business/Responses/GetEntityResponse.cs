using Contoso.Domain;

namespace Contoso.Bsl.Business.Responses
{
    public class GetEntityResponse : BaseResponse
    {
        public ViewModelBase Entity { get; set; }
    }
}
