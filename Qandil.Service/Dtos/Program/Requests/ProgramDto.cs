namespace Qandil.Service.Dtos.Program.Requests
{
    public class ProgramDto
    {
        public Guid ProgramId { get; set; }
        public string Name { get; set; }
        public int SessionNumber { get; set; }
        public string SessionDuration { get; set; }
    }
}
