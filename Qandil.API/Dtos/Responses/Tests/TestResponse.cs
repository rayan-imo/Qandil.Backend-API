using Qandil.Core.Entity;
using Qandil.Core.Enums;

namespace Qandil.API.Dtos.Responses.Tests
{
    public class TestResponse
    {
        public Guid Id { get; set; }
        public int ToutalMark { get; set; }
        public Guid LevelId { get; set; }
        public Guid SubjectId { get; set; }


        public static TestResponse Transform(Test test)
        {
            return new TestResponse
            {
                Id = test.Id,
               ToutalMark = test.ToutalMark,
               LevelId = test.LevelId,
               SubjectId = test.SubjectId,
    
            };
        }
    }
}
