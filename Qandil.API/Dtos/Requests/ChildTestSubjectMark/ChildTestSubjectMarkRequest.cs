using Qandil.Core.Common;

namespace Qandil.API.Dtos.Requests.ChildTestSubjectMark
{
    public class ChildTestSubjectMarkRequest:BaseEntity
    {
        public float ObtainMark { get; set; }
        public string? Notes { get; set; }
        public Guid SubjectId { get; set; }
       

    }
}
