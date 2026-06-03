using Qandil.Core.Common;
using Qandil.Core.Enums;

namespace Qandil.Core.Entity
{
    public class Test:BaseEntity
    {
        public string TestName { get; set; }
        public TestType testType { get; set; }
        public string Description {  get; set; }
        public DateTime TestDate { get; set; }
        public ICollection<ChildTest> ChildTests { get; set; }
        public Guid? EmployeeId { get; set; }
        public DateTime Date { get; set; }
    }
}
