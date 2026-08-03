using FluentValidation;
using Qandil.Service.Dtos.TestDto.Requests;

namespace Qandil.Service.Validation.Test
{
    public class TestValidator : AbstractValidator<TestRequestDto>
    {
        public TestValidator()
        {
            RuleFor(x => x.TotalMark)
                .GreaterThan(0).WithMessage("العلامة الكلية مطلوبة");

            RuleFor(x => x.LevelId)
                .NotEmpty().WithMessage("المرحلة التعليمية مطلوبة");

            RuleFor(x => x.SubjectId)
                .NotEmpty().WithMessage("المادة مطلوبة");
        }
    }
}
