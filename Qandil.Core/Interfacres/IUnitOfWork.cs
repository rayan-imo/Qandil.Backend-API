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
        int Complete();
        public Task<int> CompleteAsync();
    }
}
