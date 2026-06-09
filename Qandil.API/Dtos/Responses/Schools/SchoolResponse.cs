using Qandil.Core.Entity;

namespace Qandil.API.Dtos.Responses.Schools
{
    public class SchoolResponse
    {
        public Guid Id { get; set; }
        public required string SchoolName { get; set; }
        public string? PhoneNumber { get; set; }
        public required string Address { get; set; }
        public string? PrincipalName { get; set; }
        public string? Notes { get; set; }
        public static SchoolResponse Transform(School school)
        {
            return new SchoolResponse
            {
                Id = school.Id,
                SchoolName = school.SchoolName,
                PhoneNumber = school.PhoneNumber,
                Address = school.Address,
                PrincipalName = school.PrincipalName,
                Notes = school.Notes,
            };

        }
    }
}
