using Contoso.Domain.Entities;

namespace Contoso.Bsl.Business.Requests
{
    public class SaveInstructorRequest : BaseRequest
    {
        public InstructorModel Instructor { get; set; }
    }
}
