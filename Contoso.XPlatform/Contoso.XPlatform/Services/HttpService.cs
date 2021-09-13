using Akavache;
using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.XPlatform.Utils;
using System;
using System.Net.Http;
using System.Reactive.Linq;
using System.Text;
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

            var response = await GetFromCache<GetAnonymousDropDownListResponse>(jsonRequest);

            if (response != null)
                return response;

            response = await PollyHelpers.ExecutePolicyAsync
            (
                () => this.factory.PostAsync<GetAnonymousDropDownListResponse>
                (
                    url ?? "api/Dropdown/GetAnonymousDropdown",
                    jsonRequest,
                    App.BASE_URL
                )
            );

            await AddToCache(jsonRequest, response);

            return response;
        }

        public async Task<GetLookupDropDownListResponse> GetLookupDropDown(GetTypedListRequest request, string url = null)
        {
            string jsonRequest = JsonSerializer.Serialize(request);
            var response = await GetFromCache<GetLookupDropDownListResponse>(jsonRequest);

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

            await AddToCache(jsonRequest, response);

            return response;
        }

        public async Task<GetObjectDropDownListResponse> GetObjectDropDown(GetTypedListRequest request, string url = null)
        {
            string jsonRequest = JsonSerializer.Serialize(request);
            var response = await GetFromCache<GetObjectDropDownListResponse>(jsonRequest);

            if (response != null)
                return response;

            response = await PollyHelpers.ExecutePolicyAsync
            (
                () => this.factory.PostAsync<GetObjectDropDownListResponse>
                (
                    url ?? "api/Dropdown/GetObjectDropdown",
                    jsonRequest,
                    App.BASE_URL
                )
            );

            await AddToCache(jsonRequest, response);

            return response;
        }

        public Task<GetListResponse> GetList(GetTypedListRequest request, string url = null) 
            => PollyHelpers.ExecutePolicyAsync
            (
                () => this.factory.PostAsync<GetListResponse>
                (
                    url ?? "api/List/GetList",
                    JsonSerializer.Serialize(request),
                    App.BASE_URL
                )
            );

        public Task<GetEntityResponse> GetEntity(GetEntityRequest request, string url = null)
            => PollyHelpers.ExecutePolicyAsync
            (
                () => this.factory.PostAsync<GetEntityResponse>
                (
                    url ?? "api/Entity/GetEntity",
                    JsonSerializer.Serialize(request),
                    App.BASE_URL
                )
            );

        public Task<BaseResponse> SaveEntity(BaseRequest request, string url)
            => PollyHelpers.ExecutePolicyAsync
            (
                () => this.factory.PostAsync<BaseResponse>
                (
                    url,
                    JsonSerializer.Serialize(request),
                    App.BASE_URL
                )
            );

        public async Task AddToCache<T>(string cacheName, T objectToAdd)
        {
            await cache.Insert
            (
                cacheName,
                Encoding.UTF8.GetBytes(JsonSerializer.Serialize(objectToAdd)),
                DateTimeOffset.Now.AddDays(1)
            );
        }

        public async Task<T> GetFromCache<T>(string cacheName)
        {
            try
            {
                return JsonSerializer.Deserialize<T>
                (
                    Encoding.UTF8.GetString(await cache.Get(cacheName)),
                    SerializationOptions.Default
                );
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}
