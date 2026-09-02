using Qandil.Core.Common;
using Qandil.Core.Entity;
using Qandil.Core.Enums;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Specifications;
using Qandil.Service.Dtos.ChildDto.Response;
using Qandil.Service.IServices;

namespace Qandil.Service.Services
{
    public class ChildSiteService(IUnitOfWork _uow) : IChildSiteService
    {
        public async Task<Result<ChildPostTestResponseDto>> GetChildPreTestAsync(Guid childId)
        {
            if (childId == Guid.Empty)
            {
                return Result<ChildPostTestResponseDto>
                    .Failure("Child ID cannot be empty.");
            }

            var childSpec = BaseSpecification<Child>
                .Create()
                .Where(x => x.Id == childId && x.DeletedAt == null
                );

            var child = await _uow.ChildRepository.GetFirstBySpecAsync(childSpec);

            if (child == null)
                return Result<ChildPostTestResponseDto>.Failure("Child was not found.");

            var childTestSpec = BaseSpecification<ChildTest>
                .Create()
                .Where(x =>
                    x.ChildId == childId &&
                    x.DeletedAt == null && x.Type == TestType.PreTest
                )
                .OrderByDesc(x => x.Date);

            var childTest = await _uow.ChildTestRepository.GetFirstBySpecAsync(childTestSpec);

            var response = new ChildPostTestResponseDto
            {
                ChildId = child.Id,
                ChildName = $"{child.FirstName} {child.LastName}",
                MotherName=child.MotherName,
                FatherName=child.FatherName,
                JoiningDate = child.JoiningDate,
                ProgramName = child.Program?.Name ?? string.Empty
            };

            if (childTest == null)
                return Result<ChildPostTestResponseDto>.Success(response);

            var test = await _uow.TestRepository.GetByIdAsync(childTest.TestId);

            var marksSpec = BaseSpecification<SubjectMark>
                .Create()
                .Where(x =>
                    x.ChildTestId == childTest.Id &&
                    x.DeletedAt == null
                );

            var marks = await _uow.ChildTestSubjectMarkRepositoy.ListAsync(marksSpec);

            var testResponse = new PostTestResponseDto
            {
                TestId = childTest.TestId,
                TestName = test?.Name ?? string.Empty,
                TestTitle = test?.Title ?? string.Empty,
                Date = childTest.Date,
                Result = childTest.Result,
                IsPassed = childTest.IsPassed
            };

            foreach (var mark in marks)
            {
                var subject = await _uow.SubjectRepository.GetByIdAsync(mark.SubjectId);

                testResponse.SubjectMarks.Add(
                    new SubjectMarkResponseDto
                    {
                        SubjectName = subject?.Name ?? string.Empty,
                        ObtainMark = mark.ObtainMark,
                    }
                );
            }

            response.LatestTest = testResponse;

            return Result<ChildPostTestResponseDto>.Success(response);
        }
    }
}