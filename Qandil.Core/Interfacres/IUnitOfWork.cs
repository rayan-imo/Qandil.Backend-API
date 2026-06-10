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
<<<<<<< HEAD
     
=======
        IUsersRepository UsersRepository { get; }
>>>>>>> d919681 (Add AuthServices)
        int Complete();
        public Task<int> CompleteAsync();
    }
}
