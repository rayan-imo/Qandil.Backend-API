using FluentValidation;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Core.Enums;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Specifications;
using Qandil.Service.Dtos.ChildTestDto.Requests;
using Qandil.Service.IServices;

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

            if (childTest == null || childTest.DeletedAt != null)
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

            var childTest = await _uow.SubjectRepository.GetByIdAsync(id);

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
        public async Task<Result<IEnumerable<ChildLevelAverageDto>>> GetChildAveragesByChildIdAsync(Guid childId)
        {
            if (childId == Guid.Empty)
                return Result<IEnumerable<ChildLevelAverageDto>>
                    .Failure("Child ID cannot be empty.");

            var child = await _uow.ChildRepository.GetByIdAsync(childId);

            if (child == null || child.DeletedAt != null)
                return Result<IEnumerable<ChildLevelAverageDto>>
                    .Failure($"Child with ID was not found.");

            var childTests = await _uow.ChildTestRepositoy.FindAllAsync(
                x => x.ChildId == childId && x.DeletedAt == null);

            var result = childTests.GroupBy(x => x.Test.LevelId)
                .Select(level => new ChildLevelAverageDto
                {
                    LevelId = level.Key,
                    PreTests = level.Where(x => x.Type == TestType.PreTest)
                    .Select(x => new ChildTestAverageDto
                    {
                        AttemptNumber = x.AttemptNumber,
                        Marks = x.ChildTestSubjectMarks
                          .Where(m => m.DeletedAt == null)
                         .Select(m => new ChildSubjectMarkDto
                         {
                             SubjectName = m.Subject.Name,
                             Mark = m.ObtainMark,
                         }).ToList(),
                        DateTime = x.Date,
                        Average = x.ChildTestSubjectMarks
                             .Where(m => m.DeletedAt == null)
                             .Average(m => m.ObtainMark)

                    })
                        .ToList(),

                    ProTests = level
                        .Where(x => x.Type == TestType.ProTest)
                        .Select(x => new ChildTestAverageDto
                        {
                            AttemptNumber = x.AttemptNumber,
                            Marks = x.ChildTestSubjectMarks
                          .Where(m => m.DeletedAt == null)
                         .Select(m => new ChildSubjectMarkDto
                        {
                             SubjectName = m.Subject.Name,
                             Mark = m.ObtainMark,
                         }).ToList(),
                            DateTime = x.Date,
                            Average = x.ChildTestSubjectMarks
                                .Where(m => m.DeletedAt == null)
                                .Select(m => m.ObtainMark)
                                .DefaultIfEmpty()
                                .Average()
                        })
                        .ToList()
                });

            return Result<IEnumerable<ChildLevelAverageDto>>.Success(result);
        }
        public async Task<Result<ChildTestsDto>> GetChildLevelAveragesAsync(Guid childId, Guid levelId)
        {
            if (childId == Guid.Empty)
                return Result<ChildTestsDto>
                    .Failure("Child ID cannot be empty.");

            if (levelId == Guid.Empty)
                return Result<ChildTestsDto>
                    .Failure("Level ID cannot be empty.");

            var childTests = await _uow.ChildTestRepositoy.FindAllAsync(
                x => x.ChildId == childId && x.Test.LevelId == levelId && x.DeletedAt == null);

            if (!childTests.Any())
                return Result<ChildTestsDto>
                    .Failure("No tests found for this child in this level.");

            var result = new ChildTestsDto
            {
                PreTests = childTests
                    .Where(x => x.Type == TestType.PreTest)
                    .Select(x => new ChildTestAverageDto
                    {
                        AttemptNumber = x.AttemptNumber,
                        DateTime = x.Date,
                        Marks = x.ChildTestSubjectMarks
                          .Where(m => m.DeletedAt == null)
                         .Select(m => new ChildSubjectMarkDto
                         {
                             SubjectName = m.Subject.Name,
                             Mark = m.ObtainMark,
                         }).ToList(),
                        Average = x.ChildTestSubjectMarks
                            .Where(m => m.DeletedAt == null)
                            .Average(m => m.ObtainMark)
                    })
                    .ToList(),

                ProTests = childTests
                    .Where(x => x.Type == TestType.ProTest)
                    .Select(x => new ChildTestAverageDto
                    {
                        AttemptNumber = x.AttemptNumber,
                        DateTime = x.Date,
                        Marks = x.ChildTestSubjectMarks
                          .Where(m => m.DeletedAt == null)
                         .Select(m => new ChildSubjectMarkDto
                         {
                             SubjectName = m.Subject.Name,
                             Mark = m.ObtainMark,
                         }).ToList(),

                        Average = x.ChildTestSubjectMarks
                            .Where(m => m.DeletedAt == null)
                            .Average(m => m.ObtainMark)
                    })
                    .ToList()
            };

            return Result<ChildTestsDto>.Success(result);
        }
    }
}

