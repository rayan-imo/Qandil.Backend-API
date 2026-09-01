using FluentValidation;
using Qandil.Service.Dtos.TestDto.Requests;

namespace Qandil.Service.Validation.Test
{
    public class TestValidator : AbstractValidator<TestRequestDto>
    {
        public TestValidator()
        {


            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("من فضلك، قم بإدخال الاسم");


        }
    }
}
