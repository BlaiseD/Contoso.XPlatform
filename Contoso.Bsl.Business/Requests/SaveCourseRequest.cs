using Contoso.Domain.Entities;

namespace Contoso.Bsl.Business.Requests
{
    public class SaveCourseRequest : BaseRequest
    {
        public CourseModel Course { get; set; }
    }
}
