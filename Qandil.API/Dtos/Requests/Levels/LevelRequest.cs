namespace Qandil.API.Dtos.Requests.Levels
{
    public class LevelRequest
    {
        public required string LevelName { get; set; }
        public Guid ProgramId { get; set; }
    }
}
