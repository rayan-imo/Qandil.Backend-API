using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Service.Dtos.ChildTestDto.Requests;
using Qandil.Service.Dtos.ChildTestDto.Responses;

namespace Qandil.Service.IServices
{
    public interface IChildTestService
    {
        public Task<Result<PagedResult<ChildTest>>> GetAllAsync(PaginationParameter paginationParameter);
        public Task<Result<ChildTest>> GetById(Guid id);
        public Task<Result<Guid>> AddAsync(ChildTestAddRequestDto dto);
        public Task<Result<Guid>> UpdateAsync(ChildTestUpdateRequestDto dto, Guid id);
        public Task<Result<bool>> DeleteAsync(Guid id);
        public Task<Result<LevelAttemptsResponseDto>> GetChildExamHistoryAsync(Guid childId);
        public  Task<Result<TestAttemptsResponseDto>> GetChildTestAttemptsAsync(Guid childId, Guid testId);
       
    }
}

