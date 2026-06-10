
using Qandil.Core.Enums;

namespace Qandil.Services.AuthServices.Helper;

public class ApplicationUser
{
    public required string FirstName {  get; set; }
    public required string LastName { get; set; }
    public required string Email {  get; set; }
    public RoleType Role { get; set; }

}
