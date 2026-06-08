namespace Qandil.API.Dtos.Requests.EduProgram
{
    public class EduProgramRequest
    {
        public Guid ProgramId { get; set; }
        public string Name { get; set; }
        public int SessionNumber { get; set; }
        public string SessionDuration { get; set; }

    }
}
