using Qandil.Core.Common;

namespace Qandil.Core.Entity
{
    public class Level : BaseEntity
    {
        public string LevelName { get; set; }
        public Program ?Program { get; set; }
        public Guid? ProgramId { get; set; }

        public ICollection<Classroom> Classrooms { get; set; }
    }
}
