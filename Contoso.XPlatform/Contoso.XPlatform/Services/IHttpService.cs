using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using System.Threading.Tasks;

namespace Contoso.XPlatform.Services
{
    public interface IHttpService
    {
        Task<GetAnonymousDropDownListResponse> GetAnonymousDropDown(GetAnonymousDropDownListRequest request, string url = null);
        Task<GetLookupDropDownListResponse> GetLookupDropDown(GetTypedListRequest request, string url = null);
        Task<GetObjectDropDownListResponse> GetObjectDropDown(GetTypedListRequest request, string url = null);
        Task<GetListResponse> GetList(GetTypedListRequest request, string url = null);
        Task<GetEntityResponse> GetEntity(GetEntityRequest request, string url = null);
        Task<SaveEntityResponse> SaveEntity(SaveEntityRequest request, string url = null);
        Task<DeleteEntityResponse> DeleteEntity(DeleteEntityRequest request, string url = null);
    }
}
