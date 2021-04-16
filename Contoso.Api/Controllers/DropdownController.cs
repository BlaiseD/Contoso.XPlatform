using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Web.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Contoso.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Dropdown")]
    public class DropdownController : Controller
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ConfigurationOptions configurationOptions;

        public DropdownController(IHttpClientFactory clientFactory, IOptions<ConfigurationOptions> optionsAccessor)
        {
            this.clientFactory = clientFactory;
            this.configurationOptions = optionsAccessor.Value;
        }


        [HttpPost("GetAnonymousDropdown")]
        public async Task<GetAnonymousDropDownListResponse> GetAnonymousDropdown([FromBody] GetAnonymousDropDownListRequest request) 
            => await this.clientFactory.PostAsync<GetAnonymousDropDownListResponse>
            (
                "api/Dropdown/GetAnonymousDropdown",
                JsonSerializer.Serialize(request),
                this.configurationOptions.BaseBslUrl
            );

        [HttpPost("GetLookupDropdown")]
        public async Task<GetLookupDropDownListResponse> GetLookupDropDown([FromBody] GetTypedDropDownListRequest request) 
            => await this.clientFactory.PostAsync<GetLookupDropDownListResponse>
            (
                "api/Dropdown/GetLookupDropdown",
                JsonSerializer.Serialize(request),
                this.configurationOptions.BaseBslUrl
            );
    }
}
