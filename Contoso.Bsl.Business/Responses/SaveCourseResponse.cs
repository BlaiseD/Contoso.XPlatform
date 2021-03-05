using Contoso.Domain.Entities;
using LogicBuilder.Attributes;

namespace Contoso.Bsl.Business.Responses
{
    public class SaveCourseResponse : BaseResponse
    {
        [AlsoKnownAs("SaveCourseResponse_Course")]
        public CourseModel Course { get; set; }
    }
}
