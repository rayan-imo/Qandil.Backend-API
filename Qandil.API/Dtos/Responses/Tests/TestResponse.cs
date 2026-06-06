using Qandil.Core.Entity;
using Qandil.Core.Enums;

namespace Qandil.API.Dtos.Responses.Tests
{
    public class TestResponse
    {
        public string TestName { get; set; }
        public TestType TestType { get; set; }
        public string? Description { get; set; }
        public DateTime TestDate { get; set; }
        public static TestResponse Transform(Test test)
        {
            return new TestResponse
            {
                TestName = test.TestName,
                TestType = test.TestType,
                Description = test.Description,
                TestDate = test.TestDate
            };
        }
    }
}
