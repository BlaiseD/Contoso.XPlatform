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

        public Task<GetLookupDropDownListResponse> GetLookupDropDown(GetTypedDropDownListRequest request, string url = null)
        {
            return Task.FromResult(new GetLookupDropDownListResponse { DropDownList = new List<LookUpsModel> { } });
        }

        public Task<GetObjectDropDownListResponse> GetObjectDropDown(GetTypedDropDownListRequest request, string url = null)
        {
            return Task.FromResult(new GetObjectDropDownListResponse { DropDownList = new List<ViewModelBase> { } });
        }
    }
}
