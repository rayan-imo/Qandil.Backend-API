using FluentValidation;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Specifications;
using Qandil.Service.Dtos.Program.Requests;
using Qandil.Service.IServices;
using Qandil.Service.Validation.Program;

namespace Qandil.Service.Services
{
    public class EduProgramService(IUnitOfWork _uow) : IEduProgramService
    {
        public async Task<Result<PagedResult<EduProgram>>> GetAllAsync(PaginationParameter paginationParameter)
        {
            var spac = BaseSpecification<EduProgram>
                .Create()
                .Where(x => x.DeletedAt == null)
                .Paginate(paginationParameter.page, paginationParameter.pageSize);

            return Result<PagedResult<EduProgram>>.Success(await _uow.ProgramRepositoy.PagedListAsync(spac));

        }


        public async Task<Result<EduProgram>> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return Result<EduProgram>.Failure("Program ID cannot be empty.");

            var program = await _uow.ProgramRepositoy.GetByIdAsync(id);

            if (program == null || program.DeletedAt != null)
                return Result<EduProgram>.Failure($"Program with ID was not found.");

            return Result<EduProgram>.Success(program);
        }

        public async Task<Result<Guid>> AddAsync(EduProgramRequestDto dto)
        {
            await new ProgramValidator().ValidateAndThrowAsync(dto);
            var program = new EduProgram
            {
                Name = dto.Name,
                SessionDuration = dto.SessionDuration,
                SessionNumber = dto.SessionNumber,

            };

            await _uow.ProgramRepositoy.AddAsync(program);
            await _uow.CompleteAsync();
            return Result<Guid>.Success(program.Id);


        }

        public async Task<Result<Guid>> UpdateAsync(EduProgramRequestDto dto, Guid id)
        {

            if (id == Guid.Empty)
                return Result<Guid>.Failure("Program ID cannot be empty.");

            var program = await _uow.ProgramRepositoy.GetByIdAsync(id);

            if (program == null || program.DeletedAt != null)
                return Result<Guid>.Failure($"Program with ID was not found.");
            await new ProgramValidator().ValidateAndThrowAsync(dto);

            program.Name = dto.Name;
            program.SessionDuration = dto.SessionDuration;
            program.SessionNumber = dto.SessionNumber;
            await _uow.ProgramRepositoy.UpdateAsync(program);
            await _uow.CompleteAsync();
            return Result<Guid>.Success(program.Id);

        }

        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                return Result<bool>.Failure("Program ID cannot be empty.");

            var program = await _uow.ProgramRepositoy.GetByIdAsync(id);

            if (program == null || program.DeletedAt != null)
                return Result<bool>.Failure("Program with iD was not found ");

            program.DeletedAt = DateTime.UtcNow;
            await _uow.CompleteAsync();

            return Result<bool>.Success(true);

        }


    }
}
