using AutoMapper;
using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Bsl.Utils;
using Contoso.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Contoso.Bsl.Controllers
{
    [Produces("application/json")]
    [Route("api/Dropdown")]
    public class DropdownController : Controller
    {
        private readonly IMapper mapper;
        private readonly ISchoolRepository repository;

        public DropdownController(IMapper mapper, ISchoolRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        [HttpPost("GetAnonymousDropdown")]
        public async Task<GetAnonymousDropDownListResponse> GetAnonymousDropdown([FromBody] GetAnonymousDropDownListRequest request)
        {
            return await RequestHelpers.GetAnonymousSelect
            (
                request,
                repository,
                mapper
            );
        }

        [HttpPost("GetLookupDropdown")]
        public async Task<GetLookupDropDownListResponse> GetLookupDropDown([FromBody] GetTypedListRequest request)
        {
            return await RequestHelpers.GetLookupSelect
            (
                request,
                repository,
                mapper
            );
        }

        [HttpPost("GetObjectDropdown")]
        public async Task<GetObjectDropDownListResponse> GetObjectDropDown([FromBody] GetTypedListRequest request)
        {
            return await RequestHelpers.GetObjectSelect
            (
                request,
                repository,
                mapper
            );
        }
    }
}
