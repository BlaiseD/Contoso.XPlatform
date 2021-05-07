using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using System.Threading.Tasks;

namespace Contoso.XPlatform.Services
{
    public interface IHttpService
    {
        Task<GetAnonymousDropDownListResponse> GetAnonymousDropDown(GetAnonymousDropDownListRequest request, string url = null);
        Task<GetLookupDropDownListResponse> GetLookupDropDown(GetTypedDropDownListRequest request, string url = null);
        Task<GetObjectDropDownListResponse> GetObjectDropDown(GetTypedDropDownListRequest request, string url = null);
    }
}
