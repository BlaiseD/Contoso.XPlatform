using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public DropdownController(IHttpClientFactory clientFactory) 
            => this.clientFactory = clientFactory;

        [HttpPost("GetAnonymousDropdown")]
        public async Task<GetAnonymousDropDownListResponse> GetAnonymousDropdown([FromBody] GetAnonymousDropDownListRequest request) 
            => await this.clientFactory.PostAsync<GetAnonymousDropDownListResponse>
            (
                "Dropdown/GetAnonymousDropdown",
                JsonSerializer.Serialize(request)
            );

        [HttpPost("GetLookupDropdown")]
        public async Task<GetAnonymousDropDownListResponse> GetLookupDropDown([FromBody] GetTypedDropDownListRequest request) 
            => await this.clientFactory.PostAsync<GetAnonymousDropDownListResponse>
            (
                "Dropdown/GetLookupDropdown",
                JsonSerializer.Serialize(request)
            );
    }
}
