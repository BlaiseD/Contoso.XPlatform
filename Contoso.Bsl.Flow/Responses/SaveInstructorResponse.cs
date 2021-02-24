using Contoso.Domain.Entities;
using LogicBuilder.Attributes;

namespace Contoso.Bsl.Flow.Responses
{
    public class SaveInstructorResponse : BaseResponse
    {
        [AlsoKnownAs("SaveInstructorResponse_Instructor")]
        public InstructorModel Instructor { get; set; }
    }
}
