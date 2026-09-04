using Qandil.Core.Interfacres;
using Qandil.Infrastructure.Data;
using Qandil.Infrastructure.Repositories;

namespace Qandil.Infrastructure
{
    public class UnitOfWork(ApplicationDbContext _context) : IUnitOfWork
    {
        private IEmployeeRepository? _employeeRepository;
        private ILevelRepository? _levelRepository;
        private IProgramRepositoy? _programRepositoy;
        private ISchoolRepository? _schoolRepository;
        private ITestRepository? _testRepository;
        private IChildRepository? _childRepository;
        private IClassroomRepository? _classroomRepository; 
        private IDisabilityRepository? _disabilityRepository;
        private IDiagnosisRepository? _diagnosisRepository;
        private IUsersRepository? _usersRepository;
        private IUserOtpRepository? _usersOtpRepository;
        private ISubjectRepository? _subjectRepository;
        private IChildTestRepositoy? _childTestRepositoy;
        private IDiagnosisQuestionRepository? _diagnosisQuestionRepository;
        private IQuestionOptionRepository? _questionOptionRepository;
        private IAnswerRepository? _answerRepository;
        public IChildTestSubjectMarkRepositoy  _childTestSubjectMark;
        public ITestSubjectRepository _testSubject ;


        public IEmployeeRepository? EmployeeRepository=>_employeeRepository??= new EmployeeRepository(_context);
        public IProgramRepositoy? ProgramRepositoy=>_programRepositoy??= new ProgramRepository(_context);
        public ILevelRepository? LevelRepository=>_levelRepository??= new LevelRepository(_context);
        public ISchoolRepository? SchoolRepository=>_schoolRepository??= new SchoolRepository(_context);
        public ITestRepository? TestRepository => _testRepository ??= new TestRepository(_context);
        public IChildRepository? ChildRepository => _childRepository ??= new ChildRepository(_context);
        public ISubjectRepository? SubjectRepository => _subjectRepository ??= new SubjectRepository(_context);
        public IChildTestRepositoy? ChildTestRepositoy =>  _childTestRepositoy??= new ChildTestRepository(_context);
        public IClassroomRepository? ClassroomRepository => _classroomRepository??=new ClassroomRepository(_context);
        public IDisabilityRepository? DisabilityRepository=> _disabilityRepository??= new DisabilityRepository(_context);
        public IDiagnosisRepository? DiagnosisRepository=> _diagnosisRepository??= new DiagnosisRepository(_context);

        public IUsersRepository? UsersRepository=> _usersRepository??= new UsersRepository(_context);
        public IUserOtpRepository? UserOtpRepository=> _usersOtpRepository??= new UserOtpRepository(_context);
        public IDiagnosisQuestionRepository? DiagnosisQuestionRepository => _diagnosisQuestionRepository ??= new DiagnosisQuestionRepository(_context);
        public IAnswerRepository? AnswerRepository => _answerRepository ??= new AnswerRepository(_context);

         public IChildTestSubjectMarkRepositoy ChildTestSubjectMarkRepositoy => _childTestSubjectMark??new ChildTestSubjectMarkRepository(_context);
        public ITestSubjectRepository TestSubjectRepository => _testSubject ??= new TestSubjectRepository(_context);
        public IQuestionOptionRepository QuestionOptionRepository => _questionOptionRepository ??= new QuestionOptionRepository(_context);



        public int Complete()
        {
            return _context.SaveChanges();
        }
        public Task<int> CompleteAsync()
        {
            return _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
