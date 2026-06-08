using Qandil.Core.Entity;

namespace Qandil.API.Dtos.Responses.Classrooms
{
    public class ClassroomResponse
    {
        public int MaxCapacity { get; set; }
        public int CurrentCapacity { get; set; }
        public Guid? ProgramId { get; set; }
        public Guid? LevelId { get; set; }
       public Guid? EmployeeId { get; set; }
      
     public static ClassroomResponse Transform (Classroom classroom)
        {
            return new ClassroomResponse
            {
                MaxCapacity = classroom.MaxCapacity,
                CurrentCapacity = classroom.CurrentCapacity,
                ProgramId = classroom.ProgramId,
                LevelId = classroom.LevelId,
                EmployeeId = classroom.EmployeeId,
            };
        }

    }
}
