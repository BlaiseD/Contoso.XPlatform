using Contoso.Domain.Entities;
using LogicBuilder.Attributes;

namespace Contoso.Bsl.Business.Responses
{
    public class SaveDepartmentResponse : BaseResponse
    {
        [AlsoKnownAs("SaveDepartmentResponse_Department")]
        public DepartmentModel Department { get; set; }
    }
}
