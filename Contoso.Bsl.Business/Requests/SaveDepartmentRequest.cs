using Contoso.Domain.Entities;

namespace Contoso.Bsl.Business.Requests
{
    public class SaveDepartmentRequest : BaseRequest
    {
        public DepartmentModel Department { get; set; }
    }
}
