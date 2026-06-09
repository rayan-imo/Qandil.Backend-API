namespace Qandil.Service.Dtos.SchoolDto.Request
{
    public class SchoolRequestDto
    {
        public Guid SchoolId {  get; set; }
        public required string SchoolName { get; set; }
        public string? PhoneNumber { get; set; }
        public required string Address { get; set; }
        public string? PrincipalName { get; set; }
        public string? Notes { get; set; }
    }
}
