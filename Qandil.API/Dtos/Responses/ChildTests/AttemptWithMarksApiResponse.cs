namespace Qandil.API.Dtos.Responses.ChildTests
{
    public class AttemptWithMarksApiResponse
    {
        public int AttemptNumber { get; set; }
        public TestWithMarksApiResponse? PreTest { get; set; }
        public TestWithMarksApiResponse? PostTest { get; set; }
    }
}
