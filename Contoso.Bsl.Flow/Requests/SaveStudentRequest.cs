using Contoso.Domain.Entities;

namespace Contoso.Bsl.Flow.Requests
{
    public class SaveStudentRequest : BaseRequest
    {
        public StudentModel Student { get; set; }
    }
}
