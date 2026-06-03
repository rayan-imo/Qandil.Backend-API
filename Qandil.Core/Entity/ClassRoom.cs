using Qandil.Core.Common;

namespace Qandil.Core.Entity
{
    public class Classroom :BaseEntity
    {
        public int MaxCapacity { get; set; }
        public int CurrentCapacity { get; set; }
        public Guid? ProgramId { get; set; }
        public Program? Program { get; set; }
        public Guid? LevelId { get; set; }
        public Level? Level { get; set; }
        public Guid? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public Guid? ChildTestId { get; set; }
        public ChildTest? ChildTest { get; set; }

        public ICollection<Child> Children { get; set; }
    }
}
