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
    [Route("api/Instructor")]
    public class InstructorController : Controller
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ConfigurationOptions configurationOptions;

        public InstructorController(IHttpClientFactory clientFactory, IOptions<ConfigurationOptions> optionsAccessor)
        {
            this.clientFactory = clientFactory;
            this.configurationOptions = optionsAccessor.Value;
        }

        [HttpPost("Delete")]
        public async Task<DeleteEntityResponse> Delete([FromBody] DeleteEntityRequest deleteInstructorRequest)
            => await this.clientFactory.PostAsync<DeleteEntityResponse>
            (
                "api/Instructor/Delete",
                JsonSerializer.Serialize(deleteInstructorRequest),
                this.configurationOptions.BaseBslUrl
            );

        [HttpPost("Save")]
        public async Task<SaveEntityResponse> Save([FromBody] SaveEntityRequest saveInstructorRequest)
            => await this.clientFactory.PostAsync<SaveEntityResponse>
            (
                "api/Instructor/Save",
                JsonSerializer.Serialize(saveInstructorRequest),
                this.configurationOptions.BaseBslUrl
            );
    }
}
