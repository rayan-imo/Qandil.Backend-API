using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Qandil.Core.Common;
using Qandil.Core.Dtos;
using Qandil.Core.Entity;
using Qandil.Core.Enums;
using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Specifications;
using Qandil.Service.Dtos.ChildTestDto.Requests;
using Qandil.Service.Dtos.ChildTestDto.Responses;
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
            return Result<PagedResult<ChildTest>>.Success(await _uow.ChildTestRepository.PagedListAsync(spec));
        }
        public async Task<Result<ChildTest>> GetById(Guid id)   
        {
            if (id == Guid.Empty)
                return Result<ChildTest>.Failure("ChildTest ID cannot be empty.");

            var childTest = await _uow.ChildTestRepository.GetByIdAsync(id);

            if (childTest == null || childTest.DeletedAt != null)
                return Result<ChildTest>.Failure($" ChildTest with ID was not found.");

            return Result<ChildTest>.Success(childTest);
        }

        public async Task<Result<Guid>> AddAsync(ChildTestAddRequestDto dto)
        {
            await new ChildTestValidator().ValidateAndThrowAsync(dto);
            var test = await _uow.TestRepository.GetByIdAsync(dto.TestId);
            if (test == null)
                return Result<Guid>.Failure("الامتحان غير موجود ");
            var child = await _uow.ChildRepository.GetByIdAsync(dto.ChildId);
            if (child == null)
                return Result<Guid>.Failure("معرف الطفل غير موجود");
            var employee = await _uow.EmployeeRepository.GetByIdAsync(dto.EmployeeId);
            if (employee == null)
                return Result<Guid>.Failure("معرف المسؤول عن الامتحان غير موجود");

            var TestSubjectsIds = await _uow.TestRepository.GetSubjectIdsByTestIdAsync(dto.TestId);
            var submittedSubjectIds = dto.SubjectMarkDtos.Select(s => s.SubjectId).ToList();

            if (submittedSubjectIds.Any(id => !TestSubjectsIds.Contains(id)))
                return Result<Guid>.Failure("احدى المواد المرسلة لا ينتمي لهدا الاختبار");
            if (submittedSubjectIds.Count < TestSubjectsIds.Count)

                return Result<Guid>.Failure($"لم يتم ارسال جميع المواد التي تنتمي لهذا الاختبار," +
                                                 $" يجب ارسال {TestSubjectsIds.Count} من المواد   ");


            var spec = BaseSpecification<ChildTest>.Create()
         .Where(ct => ct.ChildId == dto.ChildId)
         .AndFilter(ct => ct.TestId == dto.TestId)
         .AndFilter(ct => ct.DeletedAt == null)
         .OrderByDesc(ct => ct.AttemptNumber)
         .OrderByDesc(ct => ct.Date);

            var allAttempts = await _uow.ChildTestRepository.ListAsync(spec);

            var hasNoAttempts = allAttempts == null || !allAttempts.Any();

            var attemptNumber = 0;

            if (dto.Type == TestType.PreTest)
            {
                if (hasNoAttempts)
                {
                 
                    attemptNumber = 1;
                }
                else
                {
                   
                    var maxAttemptNumber = allAttempts.Max(a => a.AttemptNumber);

                    var lastAttempts = allAttempts.Where(a => a.AttemptNumber == maxAttemptNumber).ToList();

                    var hasPostTest = lastAttempts.Any(a => a.Type == TestType.PostTest);

                    if (!hasPostTest)
                    {

                        return Result<Guid>.Failure($"يجب إضافة الاختبار البعدي للمحاولة رقم {maxAttemptNumber} أولاً");
                    }
                    else
                    {
                     
                        var postTest = lastAttempts.FirstOrDefault(a => a.Type == TestType.PostTest);

                        if (postTest != null && postTest.IsPassed)
                        {
                           
                            return Result<Guid>.Failure("تم اجتياز هذا المستوى ولا يمكن إضافة امتحان آخر لهذا المستوى");
                        }
                        else
                        {
                            
                            attemptNumber = maxAttemptNumber + 1;
                        }
                    }
                }
            }


            else if (dto.Type == TestType.PostTest)
            {
               
                if (hasNoAttempts)
                {
                    
                    return Result<Guid>.Failure("يجب إضافة الاختبار القبلي أولاً");
                }

               
                var maxAttemptNumber = allAttempts.Max(a => a.AttemptNumber);

               
                var lastAttempts = allAttempts.Where(a => a.AttemptNumber == maxAttemptNumber).ToList();

                var hasPostTest = lastAttempts.Any(a => a.Type == TestType.PostTest);

                if (!hasPostTest)
                {
                   
                    attemptNumber = maxAttemptNumber;
                }
                else
                {
                   
                    var postTest = lastAttempts.FirstOrDefault(a => a.Type == TestType.PostTest);

                    if (postTest != null && postTest.IsPassed)
                    {
                        
                        return Result<Guid>.Failure("تم اجتياز هذا المستوى ولا يمكن إضافة امتحان آخر لهذا المستوى");
                    }
                    else
                    {
                      
                        return Result<Guid>.Failure($"يجب إضافة الاختبار القبلي للمحاولة رقم {maxAttemptNumber + 1} أولاً");
                    }
                }
            }


            else if (dto.Type == TestType.PromotionTest)
            {
                if (hasNoAttempts)
                {
                    attemptNumber = 1;
                }
                else
                {

                    var lastAttempt = allAttempts.OrderByDescending(a => a.AttemptNumber).FirstOrDefault();


                    if (lastAttempt != null && lastAttempt.IsPassed)
                    {
                        return Result<Guid>.Failure("تم اجتياز الاختبار الترفيعي لهذا المستوى ولا يمكن إضافة اختبار جديد");
                    }
                    else
                    {
                        attemptNumber = lastAttempt.AttemptNumber + 1;
                    }

                }
            }


            else
            {
                return Result<Guid>.Failure("نوع الاختبار غير صحيح");
            }


            var totalMarks = dto.SubjectMarkDtos.Sum(m => m.ObtainMark);
            var subjectCount = dto.SubjectMarkDtos.Count;

            var result = (float)Math.Round(
                totalMarks / subjectCount,
                MidpointRounding.AwayFromZero
            );

            var isPassed = result >= 50;

            var childTest = new ChildTest
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                Type = dto.Type,
                Nots = dto.Nots,
                AttemptNumber = attemptNumber,
                IsPassed = isPassed,
                Result = (float)result,
                ChildId = dto.ChildId,
                TestId = dto.TestId,
                EmployeeId = dto.EmployeeId,
                ChildTestSubjectMarks = new List<SubjectMark>()

            };

            foreach (var markDto in dto.SubjectMarkDtos)
            {
                childTest.ChildTestSubjectMarks.Add(new SubjectMark
                {
                    Id = Guid.NewGuid(),
                    ObtainMark = markDto.ObtainMark,
                    Notes = markDto.Notes,
                    ChildTestId = childTest.Id,
                    SubjectId = markDto.SubjectId,

                });

            }
            await _uow.ChildTestRepository.AddAsync(childTest);
            await _uow.CompleteAsync();
            return Result<Guid>.Success(childTest.Id);
        }

        public async Task<Result<Guid>> UpdateAsync(ChildTestUpdateRequestDto dto, Guid id)
        {
            var childTest = await _uow.ChildTestRepository.GetByIdAsync(id);

            if (childTest == null)
                return Result<Guid>.Failure("الاختبار الذي تريد تعديله غير موجود");


            var child = await _uow.ChildRepository.GetByIdAsync(dto.ChildId);
            if (child == null)
                return Result<Guid>.Failure("معرف الطفل الذي تريد التعديل به غير موجود");


            var test = await _uow.TestRepository.GetByIdAsync(dto.TestId);
            if (test == null)
                return Result<Guid>.Failure("الامتحان الذي تريد التعديل به غير موجود");



            var employee = await _uow.EmployeeRepository.GetByIdAsync(dto.EmployeeId);
            if (employee == null)
                return Result<Guid>.Failure("معرف المسؤول عن الامتحان الذي تريد التعديل به  غير موجود");


            var testSubjectsIds = await _uow.TestRepository.GetSubjectIdsByTestIdAsync(dto.TestId);
            var currentSubjectIds = childTest.ChildTestSubjectMarks.Select(m => m.SubjectId).ToList();

            if (currentSubjectIds.Any(id => !testSubjectsIds.Contains(id)))
                return Result<Guid>.Failure("بعض المواد لا تنتمي للامتحان الجديد");

            if (currentSubjectIds.Count < testSubjectsIds.Count)
                return Result<Guid>.Failure($"الامتحان يحتوي على {testSubjectsIds.Count} مواد ولكن العلامات الموجودة {currentSubjectIds.Count}");


            childTest.ChildId = dto.ChildId;
            childTest.TestId = dto.TestId;
            childTest.Type = dto.Type;
            childTest.EmployeeId = dto.EmployeeId;
            childTest.Nots = dto.Nots;

            var otherAttemptsSpec = BaseSpecification<ChildTest>.Create()
                .Where(ct => ct.ChildId == dto.ChildId)
                .AndFilter(ct => ct.TestId == dto.TestId)
                .AndFilter(ct => ct.Id != id)
                .AndFilter(ct => ct.DeletedAt == null)
                .OrderByDesc(ct => ct.AttemptNumber)
                .OrderByDesc(ct => ct.Date);

            var otherAttempts = await _uow.ChildTestRepository.ListAsync(otherAttemptsSpec);
            var hasNoAttempts = otherAttempts == null || !otherAttempts.Any();

            int newAttemptNumber;

            // 7.2 تطبيق منطق الإضافة حسب النوع
            if (dto.Type == TestType.PreTest)
            {
                if (hasNoAttempts)
                {
                    newAttemptNumber = 1;
                }
                else
                {
                    var maxAttemptNumber = otherAttempts.Max(a => a.AttemptNumber);
                    var lastAttempts = otherAttempts.Where(a => a.AttemptNumber == maxAttemptNumber).ToList();
                    var hasPostTest = lastAttempts.Any(a => a.Type == TestType.PostTest);

                    if (!hasPostTest)
                    {
                        return Result<Guid>.Failure($"يجب إضافة الاختبار البعدي للمحاولة رقم {maxAttemptNumber} أولاً");
                    }
                    else
                    {
                        var postTest = lastAttempts.FirstOrDefault(a => a.Type == TestType.PostTest);
                        if (postTest != null && postTest.IsPassed)
                        {
                            return Result<Guid>.Failure("تم اجتياز هذا المستوى ولا يمكن إضافة امتحان آخر");
                        }
                        else
                        {
                            newAttemptNumber = maxAttemptNumber + 1;
                        }
                    }
                }
            }
            else if (dto.Type == TestType.PostTest)
            {
                if (hasNoAttempts)
                {
                    return Result<Guid>.Failure("يجب إضافة الاختبار القبلي أولاً");
                }
                else
                {


                    var maxAttemptNumber = otherAttempts.Max(a => a.AttemptNumber);
                    var lastAttempts = otherAttempts.Where(a => a.AttemptNumber == maxAttemptNumber).ToList();
                    var hasPreTest = lastAttempts.Any(a => a.Type == TestType.PreTest);

                    if (!hasPreTest)
                    {
                        return Result<Guid>.Failure("يجب إضافة الاختبار القبلي أولاً");
                    }
                    else
                    {
                        var hasPostTest = lastAttempts.Any(a => a.Type == TestType.PostTest);
                        if (!hasPostTest)
                        {
                            newAttemptNumber = maxAttemptNumber;
                        }
                        else
                        {
                            var postTest = lastAttempts.FirstOrDefault(a => a.Type == TestType.PostTest);
                            if (postTest != null && postTest.IsPassed)
                            {
                                return Result<Guid>.Failure("تم اجتياز هذا المستوى ولا يمكن إضافة امتحان آخر");
                            }
                            else
                            {
                                return Result<Guid>.Failure($"يجب إضافة الاختبار القبلي للمحاولة رقم {maxAttemptNumber + 1} أولاً");
                            }
                        }
                    }
                }
            }
            else if (dto.Type == TestType.PromotionTest)
            {
                if (hasNoAttempts)
                {
                    newAttemptNumber = 1;
                }
                else
                {
                    var lastPromotion = otherAttempts
                        .Where(a => a.Type == TestType.PromotionTest)
                        .OrderByDescending(a => a.AttemptNumber)
                        .FirstOrDefault();

                    if (lastPromotion != null && lastPromotion.IsPassed)
                    {
                        return Result<Guid>.Failure("تم اجتياز الاختبار الترفيعي لهذا المستوى ولا يمكن إضافة اختبار جديد");
                    }
                    else if (lastPromotion != null && !lastPromotion.IsPassed)
                    {
                        newAttemptNumber = lastPromotion.AttemptNumber + 1;
                    }
                    else
                    {
                        newAttemptNumber = 1;
                    }
                }
            }
            else
            {
                return Result<Guid>.Failure("نوع الاختبار غير صحيح");
            }

            childTest.AttemptNumber = newAttemptNumber;

            // ... تحديث باقي البيانات ...

            await _uow.ChildTestRepository.UpdateAsync(childTest);
            await _uow.CompleteAsync();

            return Result<Guid>.Success(childTest.Id);
        }
        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                return Result<bool>.Failure("ChildTest ID cannot be empty.");

            var spec = BaseSpecification<ChildTest>.Create()
                .Where(ct => ct.Id == id)
                .AndFilter(ct => ct.DeletedAt == null)
                .Include(m => m.ChildTestSubjectMarks);

            var childTest = await _uow.ChildTestRepository.GetFirstBySpecAsync(spec);



            if (childTest == null)
                return Result<bool>.Failure($"ChildTest with ID was not found.");

            foreach (var mark in childTest.ChildTestSubjectMarks)
            {
                mark.DeletedAt = DateTime.UtcNow;
            }

            childTest.DeletedAt = DateTime.UtcNow;

            await _uow.CompleteAsync();

            return Result<bool>.Success(true);
        }
        public async Task<Result<LevelAttemptsResponseDto>> GetChildExamHistoryAsync(Guid childId)
        {
            // 1. التحقق من صحة المعرف
            if (childId == Guid.Empty)
                return Result<LevelAttemptsResponseDto>.Failure("Child ID cannot be empty.");

            // 2. التحقق من وجود الطفل
            var child = await _uow.ChildRepository.GetByIdAsync(childId);
            if (child == null || child.DeletedAt != null)
                return Result<LevelAttemptsResponseDto>.Failure("Child not found.");


            var spec = BaseSpecification<ChildTest>.Create()
                .Where(ct => ct.DeletedAt == null)
                 .AndFilter(ct => ct.Child == child)
                  .Include(query => query
                     .Include(ct => ct.Test)
                          .ThenInclude(test => test.Level));

            var childTests = await _uow.ChildTestRepository.ListAsync(spec);

            if (childTests is null || !childTests.Any())
                return Result<LevelAttemptsResponseDto>.Failure("No exams found for this child.");

            var response = new LevelAttemptsResponseDto
            {
                ChildFullName = $"{child.FirstName} {child.FatherName} {child.LastName}",
                Levels = new List<LevelExamsDto>()

            };

            var groupedByLevel = childTests
               .GroupBy(ct => ct.Test.Level.LevelName)
               .OrderBy(g => g.Key);

            foreach (var levelGroup in groupedByLevel)
            {
                var levelDto = new LevelExamsDto
                {
                    LevelName = levelGroup.Key,
                    Attempts = new List<AttemptDto>()
                };

                var groupedByAttempt = levelGroup
                   .GroupBy(ct => ct.AttemptNumber)
                   .OrderBy(g => g.Key);


                foreach (var attemptGroup in groupedByAttempt)
                {
                    var attemptDto = new AttemptDto
                    {
                        AttemptNumber = attemptGroup.Key,
                        PreTest = null,
                        PostTest = null
                    };

                    foreach (var childTest in attemptGroup)
                    {
                        var testDetail = new TestDetailDto
                        {
                            Date = childTest.Date,
                            Result = childTest.Result,
                            IsPassed = childTest.IsPassed,
                            Notes = childTest.Nots,
                            EmployeeName = $"{childTest.Employee.FirstName} {childTest.Employee.LastName}" ?? "غير معروف",
                        };


                        if (childTest.Type == TestType.PreTest)
                            attemptDto.PreTest = testDetail;
                        else if (childTest.Type == TestType.PostTest)
                            attemptDto.PostTest = testDetail;
                    }

                    levelDto.Attempts.Add(attemptDto);
                }

                response.Levels.Add(levelDto);
            }

            return Result<LevelAttemptsResponseDto>.Success(response);
        }

        public async Task<Result<TestAttemptsResponseDto>> GetChildTestAttemptsAsync(Guid childId, Guid testId)
        {
            // 1. التحقق من صحة المعرفات
            if (childId == Guid.Empty)
                return Result<TestAttemptsResponseDto>.Failure("Child ID cannot be empty.");

            if (testId == Guid.Empty)
                return Result<TestAttemptsResponseDto>.Failure("Test ID cannot be empty.");

            // 2. التحقق من وجود الطفل
            var child = await _uow.ChildRepository.GetByIdAsync(childId);
            if (child == null || child.DeletedAt != null)
                return Result<TestAttemptsResponseDto>.Failure("Child not found.");

            // 3. التحقق من وجود الامتحان
            var spes = BaseSpecification<Test>.Create()
                .Where(t => t.DeletedAt == null)
                .AndFilter(t => t.Id == testId)
                .Include(t => t.Level);

            var test = await _uow.TestRepository.GetFirstBySpecAsync(spes);

            if (test == null)
                return Result<TestAttemptsResponseDto>.Failure("Test not found.");

            var spec = BaseSpecification<ChildTest>.Create()
                .Where(ct => ct.DeletedAt == null)
                 .AndFilter(ct => ct.Child == child)
                  .Include(query => query
                     .Include(ct => ct.Test)
                          .ThenInclude(test => test.Level));

            var childTests = await _uow.ChildTestRepository.ListAsync(spec);

            if (!childTests.Any())
                return Result<TestAttemptsResponseDto>.Failure("No attempts found for this child in this test.");

            // 5. بناء الـ Response
            var response = new TestAttemptsResponseDto
            {
                TestName = test.Name,
                LevelName = test.Level?.LevelName ?? "غير معروف",
                Attempts = new List<AttemptWithMarksDto>()
            };

            // 6. التجميع حسب المحاولة
            var groupedByAttempt = childTests
                .GroupBy(ct => ct.AttemptNumber)
                .OrderBy(g => g.Key);

            foreach (var attemptGroup in groupedByAttempt)
            {
                var attemptDto = new AttemptWithMarksDto
                {
                    AttemptNumber = attemptGroup.Key,
                    PreTest = null,
                    PostTest = null
                };

                foreach (var childTest in attemptGroup)
                {
                    var testDetail = new TestWithMarksResponseDto
                    {
                        ChildTestId = childTest.Id,
                        Date = childTest.Date,
                        Result = childTest.Result,
                        IsPassed = childTest.IsPassed,
                        Notes = childTest.Nots,
                        EmployeeName = $"{childTest.Employee.FirstName} {childTest.Employee.LastName}" ?? "غير معروف",
                        SubjectMarks = childTest.ChildTestSubjectMarks?
                            .Where(m => m.DeletedAt == null)
                            .Select(m => new SubjectMarkResponseDto
                            {
                                SubjectName = m.Subject?.Name ?? "غير معروف",
                                Mark = m.ObtainMark
                            }).ToList() ?? new List<SubjectMarkResponseDto>()
                    };

                    if (childTest.Type == TestType.PreTest)
                        attemptDto.PreTest = testDetail;
                    else if (childTest.Type == TestType.PostTest)
                        attemptDto.PostTest = testDetail;
                }

                response.Attempts.Add(attemptDto);
            }

            return Result<TestAttemptsResponseDto>.Success(response);
        }
    }
}

