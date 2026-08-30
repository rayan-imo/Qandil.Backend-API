namespace Qandil.API.Dtos.Responses.ChildTests
{
    public class ChildTestAttemptsResponse
    {
        public string TestName { get; set; }
        public string LevelName { get; set; }
        public List<AttemptWithMarksApiResponse> Attempts { get; set; }
    }
}
