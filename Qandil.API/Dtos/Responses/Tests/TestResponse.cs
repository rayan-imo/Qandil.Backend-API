using Qandil.Core.Entity;

namespace Qandil.API.Dtos.Responses.Tests
{
    public class TestResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool HasPreTest { get; set; }
        public Guid LevelId { get; set; }


        public static TestResponse Transform(Test test)
        {
            return new TestResponse
            {
                Id = test.Id,
                Name = test.Name,
                HasPreTest = test.HasPreTest,
                LevelId = test.LevelId,


            };
        }
    }
}
