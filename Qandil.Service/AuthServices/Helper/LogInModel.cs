using System.ComponentModel.DataAnnotations;

namespace Qandil.Services.AuthServices.Helper;
public class LogInModel
{
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    [MaxLength(50)]
    public required string Email { get; set;}

    [MaxLength(10)]
    [MinLength(8)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,10}$",
      ErrorMessage = "Password must be between 8 and 10 characters and include uppercase, lowercase, number, and special character.")]
    public required string Password { get; set; }
}
