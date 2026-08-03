using Qandil.Core.Entity;
using Qandil.Core.Enums;

namespace Qandil.API.Dtos.Responses.ChildTests
{
    public class ChildTestResponse
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public TestType Type { get; set; }
        public float Mark { get; set; }
        public string Nots { get; set; }
        public Guid ProctorId { get; set; }
        public Guid ChildId { get; set; }
        public Guid TestId { get; set; }

        public ChildTestResponse Transfoem(ChildTest childTest)
        {
            return new ChildTestResponse
            {
                Id = childTest.Id,
                Date = childTest.Date,
                Type = childTest.Type,
                Mark = childTest.Mark,
                Nots = childTest.Nots,
                ProctorId = childTest.EmployeeId,
                ChildId = childTest.ChildId,
                TestId = childTest.TestId,
            };
        }
    }
}
