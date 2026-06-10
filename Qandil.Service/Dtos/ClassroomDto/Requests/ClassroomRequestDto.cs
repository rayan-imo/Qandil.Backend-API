using Qandil.Core.Entity;

namespace Qandil.Service.Dtos.ClassRoom.Requests
{
    public class ClassroomRequestDto
    { 
        public int MaxCapacity { get; set; }
        public int CurrentCapacity { get; set; }
        public Guid? ProgramId { get; set; }
        public Guid? LevelId { get; set; }
        public Guid? EmployeeId { get; set; }
        
       
    }
}
