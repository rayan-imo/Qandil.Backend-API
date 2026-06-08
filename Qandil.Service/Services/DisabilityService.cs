using FluentValidation;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Specifications;
using Qandil.Service.Dtos.Disability.Requests;
using Qandil.Service.IServices;
using Qandil.Service.Validation.Disability;

namespace Qandil.Service.Services
{
    public class DisabilityService(IUnitOfWork _uow):IDisabilityService
    {

        public async Task<Result<PagedResult<Disability>>> GetAllAsync(PaginationParameter paginationParameter)
        {
            var spac = BaseSpecification<Disability>
                .Create()
                .Where(x => x.DeletedAt == null)
                .Paginate(paginationParameter.page, paginationParameter.pageSize);

            return Result<PagedResult<Disability>>.Success(await _uow.DisabilityRepository.PagedListAsync(spac));

        }


        public async Task<Result<Disability>> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return Result<Disability>.Failure("Disability ID cannot be empty.");

            var disability = await _uow.DisabilityRepository.GetByIdAsync(id);

            if (disability == null || disability.DeletedAt != null)
                return Result<Disability>.Failure($"Disability with ID was not found.");

            return Result<Disability>.Success(disability);
        }

        public async Task<Result<Guid>> AddAsync(DisabilityRequestDto dto)
        {
            await new DisabilityValidator().ValidateAndThrowAsync(dto);
            var disability = new Disability
            {
                Name = dto.Name,

            };

            await _uow.DisabilityRepository.AddAsync(disability);
            await _uow.CompleteAsync();
            return Result<Guid>.Success(disability.Id);


        }

        public async Task<Result<Guid>> UpdateAsync(DisabilityRequestDto dto, Guid id)
        {

            if (id == Guid.Empty)
                return Result<Guid>.Failure("Disability ID cannot be empty.");

            var disability = await _uow.DisabilityRepository.GetByIdAsync(id);

            if (disability == null || disability.DeletedAt != null)
                return Result<Guid>.Failure($"Disability with ID was not found.");
            await new DisabilityValidator().ValidateAndThrowAsync(dto);

            disability.Name = dto.Name;
            await _uow.DisabilityRepository.UpdateAsync(disability);
            await _uow.CompleteAsync();
            return Result<Guid>.Success(disability.Id);

        }

        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                return Result<bool>.Failure("Disability ID cannot be empty.");

            var disability = await _uow.DisabilityRepository.GetByIdAsync(id);

            if (disability == null || disability.DeletedAt != null)
                return Result<bool>.Failure("Disability with iD was not found ");

            disability.DeletedAt = DateTime.UtcNow;
            await _uow.CompleteAsync();

            return Result<bool>.Success(true);

        }


    }
}
