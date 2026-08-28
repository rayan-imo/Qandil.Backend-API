using FluentValidation;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.ChildTestDto.Requests;

public class ChildTestValidator : AbstractValidator<ChildTestAddRequestDto>
{
    public ChildTestValidator()
    {
        RuleFor(x => x.EmployeeId)
      .NotEmpty().WithMessage("معرف الموظف مطلوب");

        RuleFor(x => x.ChildId)
            .NotEmpty().WithMessage("معرف الطفل مطلوب");

        RuleFor(x => x.TestId)
            .NotEmpty().WithMessage("معرف الاختبار مطلوب");
        
   
        RuleFor(x => x.Type)
            .NotNull().WithMessage("نوع الاختبار مطلوب.")
            .IsInEnum().WithMessage("نوع الاختبار غير صحيح.");


        RuleFor(x => x.Nots)
            .MaximumLength(500).WithMessage("الملاحظات لا تتجاوز 500 حرف.");
          
    }
}