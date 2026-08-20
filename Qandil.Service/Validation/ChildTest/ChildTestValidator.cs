using FluentValidation;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.ChildTestDto.Requests;

public class ChildTestValidator : AbstractValidator<ChildTestRequestDto>
{
    public ChildTestValidator()
    {
        RuleFor(x => x.EmployeeId)
      .NotEmpty().WithMessage("معرف الموظف مطلوب");

        RuleFor(x => x.ChildId)
            .NotEmpty().WithMessage("معرف الطفل مطلوب");

        RuleFor(x => x.TestId)
            .NotEmpty().WithMessage("معرف الاختبار مطلوب");
        
        RuleFor(x => x.Result)
            .NotNull().WithMessage("النتيجة مطلوبة.")
            .GreaterThanOrEqualTo(0).WithMessage("النتيجة يجب أن تكون بين 0 و 100.")
            .LessThanOrEqualTo(100).WithMessage("النتيجة يجب أن تكون بين 0 و 100.");

   
        RuleFor(x => x.Type)
            .NotNull().WithMessage("نوع الاختبار مطلوب.")
            .IsInEnum().WithMessage("نوع الاختبار غير صحيح.");

       
        RuleFor(x => x.AttemptNumber)
            .NotNull().WithMessage("رقم المحاولة مطلوب.")
            .GreaterThanOrEqualTo(1).WithMessage("رقم المحاولة يجب أن يكون 1 على الأقل.")
            .LessThanOrEqualTo(5).WithMessage("الحد الأقصى للمحاولات هو 5.");


        RuleFor(x => x.Nots)
            .MaximumLength(500).WithMessage("الملاحظات لا تتجاوز 500 حرف.");
          
    }
}