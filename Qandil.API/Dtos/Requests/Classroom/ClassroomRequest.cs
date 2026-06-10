namespace Qandil.API.Dtos.Requests.Classroom
{
    public class ClassroomRequest
    {
        
        public int MaxCapacity { get; set; }
        public int CurrentCapacity { get; set; }
        public Guid? ProgramId { get; set; }
        public Guid? LevelId { get; set; }
        public Guid? EmployeeId { get; set; }
    }
}
