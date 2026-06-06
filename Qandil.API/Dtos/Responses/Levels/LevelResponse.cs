using Qandil.Core.Entity;

namespace Qandil.API.Dtos.Responses.Levels
{
    public class LevelResponse
    {
        public required string LevelName { get; set; }
        public required string ProgramName { get; set; }
        public Guid ProgramId { get; set; }

        public static LevelResponse Transform(Level level)
        {
            return new LevelResponse
            {
                LevelName = level.LevelName,
                ProgramName = level.Program.Name,
            };
        }
    }
}
