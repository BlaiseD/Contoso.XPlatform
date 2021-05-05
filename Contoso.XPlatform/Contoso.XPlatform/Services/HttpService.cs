using Akavache;
using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.XPlatform.Utils;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reactive.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Contoso.XPlatform.Services
{
    public class HttpService : IHttpService
    {
        private readonly IHttpClientFactory factory;
        private readonly IBlobCache cache;

        public HttpService(IHttpClientFactory factory)
        {
            this.factory = factory;
            cache = BlobCache.LocalMachine;
        }

        public async Task<GetAnonymousDropDownListResponse> GetAnonymousDropDown(GetAnonymousDropDownListRequest request, string url = null)
        {
            string jsonRequest = JsonSerializer.Serialize(request);

            GetAnonymousDropDownListResponse response = await GetFromCache<GetAnonymousDropDownListResponse>(jsonRequest);

            //if (response != null)
                //return response;

            response = await PollyHelpers.ExecutePolicyAsync
            (
                () => this.factory.PostAsync<GetAnonymousDropDownListResponse>
                (
                    url ?? "api/Dropdown/GetAnonymousDropdown",
                    jsonRequest,
                    App.BASE_URL
                )
            );

            await cache.InsertObject(jsonRequest, response, DateTimeOffset.Now.AddDays(1));

            return response;
        }

        public async Task<GetLookupDropDownListResponse> GetLookupDropDown(GetTypedDropDownListRequest request, string url = null)
        {
            string jsonRequest = JsonSerializer.Serialize(request);

            GetLookupDropDownListResponse response = await GetFromCache<GetLookupDropDownListResponse>(jsonRequest);

            if (response != null)
                return response;

            response = await PollyHelpers.ExecutePolicyAsync
            (
                () => this.factory.PostAsync<GetLookupDropDownListResponse>
                (
                    url ?? "api/Dropdown/GetLookupDropdown",
                    jsonRequest,
                    App.BASE_URL
                )
            );

            await cache.InsertObject(jsonRequest, response, DateTimeOffset.Now.AddDays(1));

            return response;
        }

        public async Task<T> GetFromCache<T>(string cacheName)
        {
            try
            {
                return await cache.GetObject<T>(cacheName);
            }
            catch (KeyNotFoundException)
            {
                return default;
            }
        }
    }
}
