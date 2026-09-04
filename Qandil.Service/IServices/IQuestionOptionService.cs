using Qandil.Core.Common;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.QuestionOptionsDto.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qandil.Service.IServices
{
    public interface IQuestionOptionService
    {
        Task<Result<QuestionOption>> AddOptionAsync(QuestionOptionRequestDto dto);

        Task<Result<List<QuestionOption>>> GetAllOptionsAsync();
        Task<Result<List<QuestionOption>>> GetOptionsByQuestionIdAsync(Guid questionId);
        Task<Result<QuestionOption>> GetOptionByIdAsync(Guid id);
        Task<Result<QuestionOption>> UpdateOptionAsync(Guid id, QuestionOptionRequestDto dto);

        Task<Result<bool>> DeleteOptionAsync(Guid id);
        Task<Result<bool>> DeleteOptionsByQuestionIdAsync(Guid questionId);
    }
}
