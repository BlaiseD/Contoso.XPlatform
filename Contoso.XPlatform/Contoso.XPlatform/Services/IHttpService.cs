using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using System.Threading.Tasks;

namespace Contoso.XPlatform.Services
{
    public interface IHttpService
    {
        Task<GetLookupDropDownListResponse> GetLookupDropDown(GetTypedDropDownListRequest request);
    }
}
