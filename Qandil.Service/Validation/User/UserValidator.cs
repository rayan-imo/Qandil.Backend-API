using FluentValidation;
using Qandil.Service.Dtos.UserDto.Request;

namespace Qandil.Service.Validation.User
{
    public class UserValidator : AbstractValidator<UserRequestdto>
    {
        public UserValidator()
        {
            RuleFor(x => x.Email)
              .NotEmpty().WithMessage("من فضلك، قم بإدخال الايميل")
              .EmailAddress().WithMessage("صيغة البريد الإلكتروني غير صحيحة");


        }

    }
}

