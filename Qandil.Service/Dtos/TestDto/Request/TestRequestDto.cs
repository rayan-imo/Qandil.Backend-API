using Qandil.Core.Entity;
using Qandil.Core.Enums;

namespace Qandil.Service.Dtos.TestDto.Request
{
    public class TestRequestDto
    {
        public required string TestName { get; set; }
        public required TestType TestType { get; set; }
        public string? Description { get; set; }
        public DateTime TestDate { get; set; }
       
    }
}
