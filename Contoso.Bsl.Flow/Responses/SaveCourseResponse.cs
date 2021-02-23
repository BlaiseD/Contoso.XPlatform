using Contoso.Domain.Entities;
using LogicBuilder.Attributes;

namespace Contoso.Bsl.Flow.Responses
{
    public class SaveCourseResponse : BaseResponse
    {
        [AlsoKnownAs("SaveCourseResponse_Course")]
        public CourseModel Course { get; set; }
    }
}
