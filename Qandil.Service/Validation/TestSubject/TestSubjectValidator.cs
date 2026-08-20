using FluentValidation;
using Qandil.Service.Dtos.TestSubjectDto.Request;

namespace Qandil.Service.Validation.TestSubject
{
    public class TestSubjectValidator : AbstractValidator<TestSubjectRequestDto>
    {
        public TestSubjectValidator()
        {
            RuleFor(x => x.TestId)
             .NotEmpty().WithMessage("معرف الاختبار مطلوب");

            RuleFor(x => x.SubjectId)
             .NotEmpty().WithMessage("معرف المادة مطلوب");

        }


    }
}
