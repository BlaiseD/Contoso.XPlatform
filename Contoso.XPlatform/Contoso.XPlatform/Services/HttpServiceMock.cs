using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Domain;
using Contoso.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.XPlatform.Services
{
    public class HttpServiceMock : IHttpService
    {
        public Task<GetAnonymousDropDownListResponse> GetAnonymousDropDown(GetAnonymousDropDownListRequest request, string url = null)
        {
            return Task.FromResult(new GetAnonymousDropDownListResponse { DropDownList = new List<object> { } });
        }

        public Task<GetLookupDropDownListResponse> GetLookupDropDown(GetTypedListRequest request, string url = null)
        {
            return Task.FromResult(new GetLookupDropDownListResponse { DropDownList = new List<LookUpsModel> { } });
        }

        public Task<GetObjectDropDownListResponse> GetObjectDropDown(GetTypedListRequest request, string url = null)
        {
            return Task.FromResult(new GetObjectDropDownListResponse { DropDownList = new List<ViewModelBase> { } });
        }

        public Task<GetListResponse> GetList(GetTypedListRequest request, string url = null)
        {
            return Task.FromResult(new GetListResponse { List = new List<ViewModelBase> { } });
        }
    }
}
