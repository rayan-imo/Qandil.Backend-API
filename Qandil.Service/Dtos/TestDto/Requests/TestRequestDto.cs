using Qandil.Core.Entity;

namespace Qandil.Service.Dtos.TestDto.Requests
{
    public class TestRequestDto
    {
        public string Name { get; set; }
        public bool HasPreTest { get; set; }
        public Guid LevelId { get; set; }


    }
}
