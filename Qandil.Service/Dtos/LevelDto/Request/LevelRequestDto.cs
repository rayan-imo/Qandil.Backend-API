namespace Qandil.Service.Dtos.LevelDto.Request
{
    public class LevelRequestDto
    {
       
        public required string LevelName { get; set; }
        public Guid ProgramId {  get; set; }
    }
}
