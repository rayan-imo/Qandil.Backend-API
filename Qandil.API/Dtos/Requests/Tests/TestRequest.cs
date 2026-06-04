using Qandil.Core.Enums;

namespace Qandil.API.Dtos.Requests.Tests
{
    public class TestRequest
    {
        public string TestName { get; set; }
        public TestType testType { get; set; }
        public string? Description { get; set; }
        public DateTime TestDate { get; set; }
    }
}
