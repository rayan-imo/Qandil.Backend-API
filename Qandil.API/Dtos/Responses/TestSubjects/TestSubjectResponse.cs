using Qandil.Core.Entity;

namespace Qandil.API.Dtos.Responses.TestSubjects
{

    public class TestSubjectResponse
    {
        public Guid Id { get; set; }
        public Guid TestId { get; set; }
        public Guid SubjectId { get; set; }
        public double MaxMark { get; set; }

        public static TestSubjectResponse Transform(TestSubject testSubject)
        {
            return new TestSubjectResponse
            {
                Id = testSubject.Id,
                TestId = testSubject.TestId,
                SubjectId = testSubject.SubjectId,
                MaxMark = testSubject.MaxMark
            };
        }
    }
}
