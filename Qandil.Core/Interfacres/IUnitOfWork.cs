namespace Qandil.Core.Interfacres
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository EmployeeRepository { get; }
        IProgramRepositoy ProgramRepositoy { get; }
        ILevelRepository LevelRepository { get; }
        ITestRepository TestRepository { get; }
        ISchoolRepository SchoolRepository { get; }
        IChildRepository ChildRepository { get; }
        IClassroomRepository ClassroomRepository { get; }
        IDiagnosisRepository DiagnosisRepository { get; }
        IDisabilityRepository DisabilityRepository { get; }
        IUsersRepository UsersRepository { get; }

        int Complete();
        public Task<int> CompleteAsync();
    }
}
