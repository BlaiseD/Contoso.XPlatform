using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Domain.Entities;
using Contoso.Web.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Contoso.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Student")]
    public class StudentController : Controller
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ConfigurationOptions configurationOptions;

        public StudentController(IHttpClientFactory clientFactory, IOptions<ConfigurationOptions> optionsAccessor)
        {
            this.clientFactory = clientFactory;
            this.configurationOptions = optionsAccessor.Value;
        }

        [HttpPost("Save")]
        public async Task<SaveEntityResponse> Save([FromBody] SaveEntityRequest saveStudentRequest) 
            => await this.clientFactory.PostAsync<SaveEntityResponse>
            (
                "api/Student/Save",
                JsonSerializer.Serialize(saveStudentRequest),
                this.configurationOptions.BaseBslUrl
            );

        [HttpGet]
        public async Task<IEnumerable<string>> Get() 
            => await this.clientFactory.GetAsync<IEnumerable<string>>
            (
                "api/Student",
                this.configurationOptions.BaseBslUrl
            );
    }
}
