using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Bsl.Flow;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Contoso.Bsl.Controllers
{
    [Produces("application/json")]
    [Route("api/Student")]
    public class StudentController : Controller
    {
        private readonly IFlowManager _flowManager;

        public StudentController(IFlowManager flowManager)
        {
            _flowManager = flowManager;
        }

        [HttpPost("Save")]
        public IActionResult Start([FromBody] SaveStudentRequest saveStudentRequest)
        {
            this._flowManager.FlowDataCache.Request = saveStudentRequest;
            this._flowManager.Start("savestudent");
            return Ok((SaveStudentResponse)this._flowManager.FlowDataCache.Response);
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "a", "b"};
        }
    }
}
