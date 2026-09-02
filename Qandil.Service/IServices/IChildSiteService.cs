using Qandil.Core.Common;
using Qandil.Service.Dtos.ChildDto.Response;

namespace Qandil.Service.IServices
{
    public interface IChildSiteService
    {
        Task<Result<ChildPostTestResponseDto>> GetChildPreTestAsync(Guid childId);
    }
}

