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
    [Route("api/Department")]
    public class DepartmentController : Controller
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ConfigurationOptions configurationOptions;

        public DepartmentController(IHttpClientFactory clientFactory, IOptions<ConfigurationOptions> optionsAccessor)
        {
            this.clientFactory = clientFactory;
            this.configurationOptions = optionsAccessor.Value;
        }

        [HttpPost("Delete")]
        public async Task<DeleteEntityResponse> Delete([FromBody] DeleteEntityRequest deleteDepartmentRequest)
            => await this.clientFactory.PostAsync<DeleteEntityResponse>
            (
                "api/Department/Delete",
                JsonSerializer.Serialize(deleteDepartmentRequest),
                this.configurationOptions.BaseBslUrl
            );

        [HttpPost("Save")]
        public async Task<SaveEntityResponse> Save([FromBody] SaveEntityRequest saveDepartmentRequest)
            => await this.clientFactory.PostAsync<SaveEntityResponse>
            (
                "api/Department/Save",
                JsonSerializer.Serialize(saveDepartmentRequest),
                this.configurationOptions.BaseBslUrl
            );
    }
}