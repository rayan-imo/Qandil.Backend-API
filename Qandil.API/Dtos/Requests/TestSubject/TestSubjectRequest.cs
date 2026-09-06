namespace Qandil.API.Dtos.Requests.TestSubject
{
    public class TestSubjectRequest
    {
        public Guid TestId { get; set; }
        public Guid SubjectId { get; set; }
        public double MaxMark { get; set; }
    }
}
