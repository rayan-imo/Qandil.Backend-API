using FluentValidation;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Specifications;
using Qandil.Service.Dtos.ChildTestDto.Requests;
using Qandil.Service.Dtos.SubjectDto.Request;
using Qandil.Service.IServices;
using Qandil.Service.Validation.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Qandil.Service.Services
{
    public class ChildTestService(IUnitOfWork _uow) : IChildTestService
    {
        public async Task<Result<PagedResult<ChildTest>>> GetAllAsync(PaginationParameter paginationParameter)
        {
            var spec = BaseSpecification<ChildTest>
                .Create()
                .Where(x => x.DeletedAt == null)
                .Paginate(paginationParameter.page, paginationParameter.pageSize);
            return Result<PagedResult<ChildTest>>.Success(await _uow.ChildTestRepositoy.PagedListAsync(spec));
        }
        public async Task<Result<ChildTest>> GetById(Guid id)
        {
            if (id == Guid.Empty)
                return Result<ChildTest>.Failure("ChildTest ID cannot be empty.");

            var childTest = await _uow.ChildTestRepositoy.GetByIdAsync(id);

            if (childTest == null || childTest.DeletedAt != null)
                return Result<ChildTest>.Failure($" ChildTest with ID was not found.");

            return Result<ChildTest>.Success(childTest);
        }

        public async Task<Result<Guid>> AddAsync(ChildTestRequestDto dto)
        {
            await new ChildTestValidator().ValidateAndThrowAsync(dto);
            var childTest = new ChildTest
            {
                Date = dto.Date,
                Type = dto.Type,
                Nots = dto.Nots,
                AttemptNumber = dto.AttemptNumber,
                IsPassed = dto.IsPassed,
                Result = dto.Result,
                ChildId = dto.ChildId,
                TestId = dto.TestId,
                EmployeeId = dto.EmployeeId,

            };
            await _uow.ChildTestRepositoy.AddAsync(childTest);
            await _uow.CompleteAsync();
            return Result<Guid>.Success(childTest.Id);
        }
        public async Task<Result<Guid>> UpdateAsync(ChildTestRequestDto dto, Guid id)
        {
            if (id == Guid.Empty)
                return Result<Guid>.Failure("ChildTest ID cannot be empty.");

            var childTest = await _uow.ChildTestRepositoy.GetByIdAsync(id);

            if (childTest == null || childTest != null)
                return Result<Guid>.Failure($"ChildTest with ID was not found.");

            await new ChildTestValidator().ValidateAndThrowAsync(dto);
            childTest.Date = dto.Date;
            childTest.Type = dto.Type;
            childTest.Nots = dto.Nots;
            childTest.AttemptNumber = dto.AttemptNumber;
            childTest.IsPassed = dto.IsPassed;
            childTest.Result = dto.Result;
            childTest.ChildId = dto.ChildId;
            childTest.TestId = dto.TestId;
            childTest.EmployeeId = dto.EmployeeId;

            await _uow.ChildTestRepositoy.UpdateAsync(childTest);
            await _uow.CompleteAsync();
            return Result<Guid>.Success(childTest.Id);

        }
        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                return Result<bool>.Failure("ChildTest ID cannot be empty.");

            var childTest= await _uow.SubjectRepository.GetByIdAsync(id);

            if (childTest == null || childTest.DeletedAt != null)
                return Result<bool>.Failure($"ChildTest with ID was not found.");


            var childTestSubjectMarks = await _uow.ChildTestSubjectMarkRepositoy.FindAllAsync
                (x => x.SubjectId == id && x.DeletedAt != null);

            foreach (var childTestSubjectMark in childTestSubjectMarks)
                childTestSubjectMark.DeletedAt = DateTime.UtcNow;

            childTest.DeletedAt = DateTime.UtcNow;
            await _uow.CompleteAsync();

            return Result<bool>.Success(true);
        }
    }
}

