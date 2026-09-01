namespace Qandil.Core.Interfacres
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository EmployeeRepository { get; }
        IProgramRepositoy ProgramRepositoy { get; }
        ILevelRepository LevelRepository { get; }
        ITestRepository TestRepository { get; }
        ISubjectRepository SubjectRepository { get; }
        ISchoolRepository SchoolRepository { get; }
        IChildRepository ChildRepository { get; }
        IClassroomRepository ClassroomRepository { get; }
        ITestSubjectRepository TestSubjectRepository { get; }
        IChildTestSubjectMarkRepositoy ChildTestSubjectMarkRepositoy { get; }
        IChildTestRepositoy ChildTestRepositoy { get; }
        IDiagnosisRepository DiagnosisRepository { get; }
        IDisabilityRepository DisabilityRepository { get; }
        IUsersRepository UsersRepository { get; }
        IUserOtpRepository UserOtpRepository { get; }
        IQuestionRepository QuestionRepository { get; }
        IAnswerRepository AnswerRepository { get; }
        IEvaluationCardRepository EvaluationCardRepository { get; }

        int Complete();
        public Task<int> CompleteAsync();
    }
}
