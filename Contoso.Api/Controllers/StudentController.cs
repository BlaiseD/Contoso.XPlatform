using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
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
        public async Task<SaveStudentResponse> Save([FromBody] SaveStudentRequest saveStudentRequest) 
            => await this.clientFactory.PostAsync<SaveStudentResponse>
            (
                "Student/Save",
                JsonSerializer.Serialize(saveStudentRequest),
                this.configurationOptions
            );

        [HttpGet]
        public async Task<IEnumerable<string>> Get() 
            => await this.clientFactory.GetAsync<IEnumerable<string>>
            (
                "Student",
                this.configurationOptions
            );
    }
}
