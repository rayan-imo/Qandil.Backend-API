namespace Qandil.Service.Dtos.ChildTestSubjectMarkDto.Request
{
    public class ChildTestSubjectMarkRequestDto
    {
        public double ObtainMark { get; set; }
        public string? Notes { get; set; }
        public Guid SubjectId { get; set; }
        public Guid EmployeeId { get; set; }
    }
}

