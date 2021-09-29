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
    [Route("api/Course")]
    public class CourseController : Controller
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ConfigurationOptions configurationOptions;

        public CourseController(IHttpClientFactory clientFactory, IOptions<ConfigurationOptions> optionsAccessor)
        {
            this.clientFactory = clientFactory;
            this.configurationOptions = optionsAccessor.Value;
        }

        [HttpPost("Delete")]
        public async Task<DeleteEntityResponse> Delete([FromBody] DeleteEntityRequest deleteCourseRequest)
            => await this.clientFactory.PostAsync<DeleteEntityResponse>
            (
                "api/Course/Delete",
                JsonSerializer.Serialize(deleteCourseRequest),
                this.configurationOptions.BaseBslUrl
            );

        [HttpPost("Save")]
        public async Task<SaveEntityResponse> Save([FromBody] SaveEntityRequest saveCourseRequest)
            => await this.clientFactory.PostAsync<SaveEntityResponse>
            (
                "api/Course/Save",
                JsonSerializer.Serialize(saveCourseRequest),
                this.configurationOptions.BaseBslUrl
            );
    }
}