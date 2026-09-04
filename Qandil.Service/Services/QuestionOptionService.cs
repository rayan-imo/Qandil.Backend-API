using FluentValidation;
using Qandil.Core.Common;
using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Specifications;
using Qandil.Service.Dtos.QuestionOptionsDto.Requests;
using Qandil.Service.IServices;
using Qandil.Service.Validation.QuestionOption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qandil.Service.Services
{
    public class QuestionOptionService(IUnitOfWork _uow): IQuestionOptionService
    {


        public async Task<Result<QuestionOption>> AddOptionAsync(QuestionOptionRequestDto dto)
        {
           
            await new QuestionOptionValidator().ValidateAndThrowAsync(dto);

            var question = await _uow.DiagnosisQuestionRepository.GetByIdAsync(dto.DiagnosisQuestionId);
            if (question == null || question.DeletedAt != null)
                return Result<QuestionOption>.Failure("السؤال غير موجود");

         
            var spec = BaseSpecification<QuestionOption>.Create()
                .Where(x => x.DeletedAt == null
                    && x.DiagnosisQuestionId == dto.DiagnosisQuestionId
                    && x.Text == dto.Text);

            var existingOption = await _uow.QuestionOptionRepository.GetFirstBySpecAsync(spec);
            if (existingOption != null)
                return Result<QuestionOption>.Failure("يوجد بالفعل خيار بنفس النص");

           
            var option = new QuestionOption
            {
                Id = Guid.NewGuid(),
                Text = dto.Text,
                Value = dto.Value ?? 0,
                Order = dto.Order,
                DiagnosisQuestionId = dto.DiagnosisQuestionId,
                CreatedAt = DateTime.UtcNow
            };

          
            await _uow.QuestionOptionRepository.AddAsync(option);
            await _uow.CompleteAsync();

         
            var specWithInclude = BaseSpecification<QuestionOption>.Create()
                .Where(x => x.Id == option.Id && x.DeletedAt == null)
                .Include(x => x.Question);

            var optionWithQuestion = await _uow.QuestionOptionRepository.GetFirstBySpecAsync(specWithInclude);

            return Result<QuestionOption>.Success(optionWithQuestion ?? option);
        }

      
        public async Task<Result<List<QuestionOption>>> GetAllOptionsAsync()
        {
            var spec = BaseSpecification<QuestionOption>.Create()
                .Where(x => x.DeletedAt == null)
                .Include(x => x.Question)
                .OrderByAsc(x => x.DiagnosisQuestionId)
                .OrderByAsc(x => x.Order);

            var options = await _uow.QuestionOptionRepository.ListAsync(spec);

            if (options == null || !options.Any())
                return Result<List<QuestionOption>>.Success(new List<QuestionOption>());

            return Result<List<QuestionOption>>.Success(options.ToList());
        }

        // ============================================
        // 3. جلب خيارات سؤال محدد
        // ============================================
        public async Task<Result<List<QuestionOption>>> GetOptionsByQuestionIdAsync(Guid questionId)
        {
            if (questionId == Guid.Empty)
                return Result<List<QuestionOption>>.Failure("معرف السؤال غير صالح");

            var spec = BaseSpecification<QuestionOption>.Create()
                .Where(x => x.DeletedAt == null
                    && x.DiagnosisQuestionId == questionId)
                .Include(x => x.Question)
                .OrderByAsc(x => x.Order);

            var options = await _uow.QuestionOptionRepository.ListAsync(spec);

            if (options == null || !options.Any())
                return Result<List<QuestionOption>>.Success(new List<QuestionOption>());

            return Result<List<QuestionOption>>.Success(options.ToList());
        }

    
        public async Task<Result<QuestionOption>> GetOptionByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                return Result<QuestionOption>.Failure("معرف الخيار غير صالح");

            var spec = BaseSpecification<QuestionOption>.Create()
                .Where(x => x.DeletedAt == null && x.Id == id)
                .Include(x => x.Question);

            var option = await _uow.QuestionOptionRepository.GetFirstBySpecAsync(spec);

            if (option == null)
                return Result<QuestionOption>.Failure("الخيار غير موجود");

            return Result<QuestionOption>.Success(option);
        }

       
        public async Task<Result<QuestionOption>> UpdateOptionAsync(Guid id, QuestionOptionRequestDto dto)
        {
          
            if (id == Guid.Empty)
                return Result<QuestionOption>.Failure("معرف الخيار غير صالح");

            await new QuestionOptionValidator().ValidateAndThrowAsync(dto);

           
            var spec = BaseSpecification<QuestionOption>.Create()
                .Where(x => x.DeletedAt == null && x.Id == id)
                .Include(x => x.Question);

            var option = await _uow.QuestionOptionRepository.GetFirstBySpecAsync(spec);

            if (option == null)
                return Result<QuestionOption>.Failure("الخيار غير موجود");

          
            var question = await _uow.DiagnosisQuestionRepository.GetByIdAsync(dto.DiagnosisQuestionId);
            if (question == null || question.DeletedAt != null)
                return Result<QuestionOption>.Failure("السؤال غير موجود");

          
            var duplicateSpec = BaseSpecification<QuestionOption>.Create()
                .Where(x => x.DeletedAt == null
                    && x.DiagnosisQuestionId == dto.DiagnosisQuestionId
                    && x.Text == dto.Text
                    && x.Id != id);

            var existingOption = await _uow.QuestionOptionRepository.GetFirstBySpecAsync(duplicateSpec);
            if (existingOption != null)
                return Result<QuestionOption>.Failure("يوجد بالفعل خيار بنفس النص");

          
            option.Text = dto.Text;
            option.Value = dto.Value ?? 0;
            option.Order = dto.Order;
            option.DiagnosisQuestionId = dto.DiagnosisQuestionId;
         

       
            await _uow.QuestionOptionRepository.UpdateAsync(option);
            await _uow.CompleteAsync();

        
            var specWithInclude = BaseSpecification<QuestionOption>.Create()
                .Where(x => x.Id == option.Id && x.DeletedAt == null)
                .Include(x => x.Question);

            var updatedOption = await _uow.QuestionOptionRepository.GetFirstBySpecAsync(specWithInclude);

           
            return Result<QuestionOption>.Success(updatedOption ?? option);
        }

     
        public async Task<Result<bool>> DeleteOptionAsync(Guid id)
        {
            if (id == Guid.Empty)
                return Result<bool>.Failure("معرف الخيار غير صالح");

            var spec = BaseSpecification<QuestionOption>.Create()
                .Where(x => x.DeletedAt == null && x.Id == id);

            var option = await _uow.QuestionOptionRepository.GetFirstBySpecAsync(spec);

            if (option == null)
                return Result<bool>.Failure("الخيار غير موجود");

            // Soft Delete
            option.DeletedAt = DateTime.UtcNow;

            await _uow.QuestionOptionRepository.UpdateAsync(option);
            await _uow.CompleteAsync();

            return Result<bool>.Success(true);
        }

        // ============================================
        // . حذف جميع خيارات سؤال محدد
        // ============================================
        public async Task<Result<bool>> DeleteOptionsByQuestionIdAsync(Guid questionId)
        {
            if (questionId == Guid.Empty)
                return Result<bool>.Failure("معرف السؤال غير صالح");

            var spec = BaseSpecification<QuestionOption>.Create()
                .Where(x => x.DeletedAt == null
                    && x.DiagnosisQuestionId == questionId);

            var options = await _uow.QuestionOptionRepository.ListAsync(spec);

            if (options == null || !options.Any())
                return Result<bool>.Success(true);

            // Soft Delete للجميع
            foreach (var option in options)
            {
                option.DeletedAt = DateTime.UtcNow;
                await _uow.QuestionOptionRepository.UpdateAsync(option);  
            }

           
            await _uow.CompleteAsync();

            return Result<bool>.Success(true);
        }

    }
}
