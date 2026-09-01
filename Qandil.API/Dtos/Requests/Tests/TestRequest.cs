using Qandil.Core.Entity;
using Qandil.Core.Enums;

namespace Qandil.API.Dtos.Requests.Tests
{
    public class TestRequest
    {
        public string Name { get; set; }
        public bool HasPreTest { get; set; }
        public Guid LevelId { get; set; }

    }
}
