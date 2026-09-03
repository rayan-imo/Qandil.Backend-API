
using Qandil.API.Dtos.Requests.ChildTestSubjectMark;
using Qandil.Core.Enums;

namespace Qandil.API.Dtos.Requests.ChildTests
{
    public class ChildTestAddRequest
    {
        public TestType Type { get; set; }
        public string? Nots { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid ChildId { get; set; }
        public Guid TestId { get; set; }
        public List<ChildTestSubjectMarkRequest> SubjectMarks { get; set; }
    }
}
