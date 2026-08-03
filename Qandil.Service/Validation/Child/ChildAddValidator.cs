using FluentValidation;
using Qandil.Service.Dtos.ChildDto.Request;

namespace Qandil.Service.Validation.Child
{
    public class ChildAddValidator : AbstractValidator<ChildAddRequesDto>
    {
        public ChildAddValidator()
        {
            

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("الاسم الأول مطلوب")
                .MaximumLength(100).WithMessage("الاسم الأول لا يتجاوز 100 حرف")
                .Matches(@"^[\p{L}\s]+$").WithMessage("الاسم يجب أن يحتوي على حروف فقط");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("الاسم الأخير مطلوب")
                .MaximumLength(100).WithMessage("الاسم الأخير لا يتجاوز 100 حرف")
                .Matches(@"^[\p{L}\s]+$").WithMessage("الاسم يجب أن يحتوي على حروف فقط");

            RuleFor(x => x.MotherName)
                .NotEmpty().WithMessage("اسم الأم مطلوب")
                .MaximumLength(100).WithMessage("اسم الأم لا يتجاوز 100 حرف")
                .Matches(@"^[\p{L}\s]+$").WithMessage("الاسم يجب أن يحتوي على حروف فقط");

            RuleFor(x => x.FatherName)
                .NotEmpty().WithMessage("اسم الأب مطلوب")
                .MaximumLength(100).WithMessage("اسم الأب لا يتجاوز 100 حرف")
                .Matches(@"^[\p{L}\s]+$").WithMessage("الاسم يجب أن يحتوي على حروف فقط");

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("الجنس مطلوب")
                .IsInEnum().WithMessage("الجنس غير صحيح");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("تاريخ الميلاد مطلوب")
                .LessThan(DateTime.Now).WithMessage("تاريخ الميلاد يجب أن يكون في الماضي");

            RuleFor(x => x.PlaceOfBearth)
                .NotEmpty().WithMessage("مكان الميلاد مطلوب")
                .MaximumLength(200).WithMessage("مكان الميلاد لا يتجاوز 200 حرف");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("العنوان مطلوب")
                .MaximumLength(300).WithMessage("العنوان لا يتجاوز 300 حرف");

            RuleFor(x => x.JoiningDate)
                .NotEmpty().WithMessage("تاريخ الانضمام مطلوب")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("تاريخ الانضمام لا يمكن أن يكون في المستقبل");

            RuleFor(x => x.HasDisability)
                .NotNull().WithMessage("حقل الإعاقة مطلوب");

            RuleFor(x => x.IsEnrolledInSchool)
                .NotNull().WithMessage("حقل التسجيل في المدرسة مطلوب");

            // ========== الحقول الشرطية (Conditional) ==========

            RuleFor(x => x.SchoolName)
                .NotEmpty().WithMessage("اسم المدرسة مطلوب عند التسجيل")
                .MaximumLength(100).WithMessage("اسم المدرسة لا يتجاوز 100 حرف")
                .When(x => x.IsEnrolledInSchool == true);

            RuleFor(x => x.SchoolGrade)
                .NotEmpty().WithMessage("الصف الدراسي مطلوب عند التسجيل")
                .MaximumLength(50).WithMessage("الصف الدراسي لا يتجاوز 50 حرف")
                .When(x => x.IsEnrolledInSchool == true);

            // ========== الحقول الاختيارية (Optional) ==========

            RuleFor(x => x.FatherJob)
                .MaximumLength(100).WithMessage("وظيفة الأب لا تتجاوز 100 حرف")
                .When(x => !string.IsNullOrEmpty(x.FatherJob));

            RuleFor(x => x.MotherJob)
                .MaximumLength(100).WithMessage("وظيفة الأم لا تتجاوز 100 حرف")
                .When(x => !string.IsNullOrEmpty(x.MotherJob));

            RuleFor(x => x.FamilyMembers)
                .GreaterThan(0).WithMessage("عدد أفراد الأسرة يجب أن يكون أكبر من 0")
                .LessThanOrEqualTo(20).WithMessage("عدد أفراد الأسرة لا يتجاوز 20")
                .When(x => x.FamilyMembers.HasValue);

            // ========== الحقول الإضافية في Update فقط ==========


        }
    }
}
