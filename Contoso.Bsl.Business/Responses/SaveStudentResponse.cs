using Contoso.Domain.Entities;
using LogicBuilder.Attributes;

namespace Contoso.Bsl.Business.Responses
{
    public class SaveStudentResponse : BaseResponse
    {
        [AlsoKnownAs("SaveStudentResponse_Student")]
        public StudentModel Student { get; set; }
    }
}
