using Qandil.Core.Common;

namespace Qandil.Core.Entity
{
    public class EduProgram: BaseEntity 
    {
        public string Name { get; set; }
        public int SessionNumber { get; set; }
        public string SessionDuration { get; set; }
        public ICollection<Level> Levels { get; set; }
        public ICollection<Classroom> Classrooms { get; set; }
        public ICollection<Child> Children { get; set; }

    }

}
  