using Qandil.Core.Entity;

namespace Qandil.API.Dtos.Responses.SubjectMarks
{
    public class SubjectMarkResponse
    {
        public Guid Id { get; set; }
        public float ObtainMark { get; set; }
        public string? Notes { get; set; }
        public Guid ChildTestId { get; set; }
        public Guid SubjectId { get; set; }

        public static SubjectMarkResponse Transform(SubjectMark subjectMark)
        {
            return new SubjectMarkResponse
            {
                Id = subjectMark.Id,
                ObtainMark = subjectMark.ObtainMark,
                Notes = subjectMark.Notes,
                ChildTestId = subjectMark.ChildTestId,
                SubjectId = subjectMark.SubjectId
            };
        }
    }
}
