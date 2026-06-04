namespace Qandil.API.Dtos.Requests.Schools
{
    public class SchoolRequest
    {
        public required string SchoolName { get; set; }
        public string? PhoneNumber { get; set; }
        public required string Address { get; set; }
        public string? PrincipalName { get; set; }
        public string? Notes { get; set; }
    }
}
