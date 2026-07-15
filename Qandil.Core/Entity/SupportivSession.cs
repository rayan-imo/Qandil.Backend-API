using Qandil.Core.Common;

namespace Qandil.Core.Entity
{
    public class SupportivSession:BaseEntity
    {
        public string Name { get; set; }
        public string SessionDuration { get; set; }
        public Child Child { get; set; }
        public Guid? ChildId { get; set; }

       // public SessionType SessionType  { get; set; }


    }
}
