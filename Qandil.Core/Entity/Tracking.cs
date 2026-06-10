using Qandil.Core.Common;

namespace Qandil.Core.Entity
{
    public class Tracking : BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsCurrentSchool { get; set; }
        public Guid? ChildId { get; set; }
        public Child? Child { get; set; }
        public Guid? SchoolId { get; set; }
        public School? School { get; set; }
    }
}
