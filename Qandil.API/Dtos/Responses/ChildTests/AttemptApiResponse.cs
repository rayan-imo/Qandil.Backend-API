namespace Qandil.API.Dtos.Responses.ChildTests
{
    public class AttemptApiResponse
    {
        public int AttemptNumber { get; set; }
        public TestDetailApiResponse? PreTest { get; set; }
        public TestDetailApiResponse? PostTest { get; set; }
    }
}
