using FluentValidation;
using Qandil.Service.Dtos.TestDto.Request;

namespace Qandil.Service.Validation.Test
{
    public class TestValidator: AbstractValidator<TestRequestDto>
    {
        public TestValidator()
        {
            RuleFor(x => x.TestName)
              .NotEmpty().WithMessage("من فضلك، قم بإدخال اسم الاختبار");
            RuleFor(x => x.TestType)
                .NotEmpty().WithMessage("من فضلك، قم بإدخال نوع الاختبار");

        }
    }
}
