using Contoso.Domain.Entities;

namespace Contoso.Bsl.Business.Requests
{
    public class SaveStudentRequest : BaseRequest
    {
        public StudentModel Student { get; set; }
    }
}
