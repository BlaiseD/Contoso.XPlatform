using Contoso.Domain.Entities;

namespace Contoso.Bsl.Flow.Responses
{
    public class AddStudentResponse : BaseResponse
    {
        public StudentModel Student { get; set; }
    }
}
