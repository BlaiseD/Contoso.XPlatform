using Contoso.Domain.Entities;

namespace Contoso.Bsl.Flow.Requests
{
    public class SaveInstructorRequest : BaseRequest
    {
        public InstructorModel Instructor { get; set; }
    }
}
