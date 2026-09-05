using FluentValidation;
using Qandil.Core.Enums;
using Qandil.Service.Dtos.DiagnosisQuestionDto.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qandil.Service.Validation.DiagnosisQuestions
{
    public class DiagnosisQuestionValidator : AbstractValidator<DiagnosisQuestionRequestDto>
    {
        public DiagnosisQuestionValidator()
        {
            // ===== التحقق من الحقول الأساسية =====
           

            RuleFor(x => x.SubTitle)
                .NotEmpty().WithMessage("العنوان الجانبي مطلوب")
                .MaximumLength(200).WithMessage("العنوان الجانبي لا يتجاوز 200 حرف");

            RuleFor(x => x.QuestionText)
                .NotEmpty().WithMessage("نص السؤال مطلوب")
                .MaximumLength(500).WithMessage("نص السؤال لا يتجاوز 500 حرف");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("نوع السؤال غير صالح");

            RuleFor(x => x.Order)
                .GreaterThanOrEqualTo(0).WithMessage("الترتيب يجب أن يكون 0 أو أكثر");
             
            // ===== التحقق من الخيارات =====
            When(x => x.Type == QuestionType.Options,() =>
            {
                RuleFor(x => x.Options)  
                    .NotNull().WithMessage("الخيارات مطلوبة")
                    .Must(o => o != null && o.Any()).WithMessage("يجب إضافة خيارين واحد على الأقل")
                    .Must(o => o != null && o.Count >= 2).WithMessage("يجب إضافة خيارين على الأقل");

                RuleForEach(x => x.Options)
                    .Must(o => !string.IsNullOrEmpty(o.Text)).WithMessage("نص الخيار مطلوب")
                    .Must(o => o.Text.Length <= 100).WithMessage("نص الخيار لا يتجاوز 100 حرف")
                    ;

            });

            // ===== إذا كان النوع Boolean، ما يحتاج خيارات =====
            When(x => x.Type == QuestionType.Boolean, () =>
            {
                RuleFor(x => x.Options)
                    .Must(o => o == null || !o.Any())
                    .WithMessage("الأسئلة من نوع Boolean لا تحتاج خيارات");
            });

            // ===== إذا كان النوع Text، ما يحتاج خيارات =====
            When(x => x.Type == QuestionType.Text, () =>
            {
                RuleFor(x => x.Options)
                    .Must(o => o == null || !o.Any())
                    .WithMessage("الأسئلة من نوع Text لا تحتاج خيارات");
            });
        }
    }
}
