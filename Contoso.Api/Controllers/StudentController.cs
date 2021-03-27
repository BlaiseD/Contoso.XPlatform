using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Microsoft.AspNetCore.Mvc;
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

        public StudentController(IHttpClientFactory clientFactory) 
            => this.clientFactory = clientFactory;

        [HttpPost("Save")]
        public async Task<SaveStudentResponse> Save([FromBody] SaveStudentRequest saveStudentRequest) 
            => await this.clientFactory.PostAsync<SaveStudentResponse>
            (
                "Student/Save",
                JsonSerializer.Serialize(saveStudentRequest)
            );

        [HttpGet]
        public async Task<IEnumerable<string>> Get() 
            => await this.clientFactory.GetAsync<IEnumerable<string>>
            (
                "Student"
            );
    }
}
