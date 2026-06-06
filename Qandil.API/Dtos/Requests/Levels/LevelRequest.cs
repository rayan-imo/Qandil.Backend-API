namespace Qandil.API.Dtos.Requests.Levels
{
    public class LevelRequest
    {
        public required string LevelName { get; set; }
        public required string ProgramName { get; set; }
        public Guid ProgramId { get; set; }
    }
}
