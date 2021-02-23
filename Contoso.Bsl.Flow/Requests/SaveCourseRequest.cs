using Contoso.Domain.Entities;

namespace Contoso.Bsl.Flow.Requests
{
    public class SaveCourseRequest : BaseRequest
    {
        public CourseModel Course { get; set; }
    }
}
