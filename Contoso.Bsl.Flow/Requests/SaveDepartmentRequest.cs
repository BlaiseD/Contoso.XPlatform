using Contoso.Domain.Entities;

namespace Contoso.Bsl.Flow.Requests
{
    public class SaveDepartmentRequest : BaseRequest
    {
        public DepartmentModel Department { get; set; }
    }
}
