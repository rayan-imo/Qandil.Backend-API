using Microsoft.EntityFrameworkCore;
using Qandil.Core.Entity;

namespace Qandil.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
                
        }
        public DbSet<Child> Childs { get; set; }
        public DbSet<Disability> Disabilities { get; set; }
        public DbSet<DiagnosisDisability> DiagnosisDisabilities { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EduProgram> Programs { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<SupportivSession> SupportivSessions { get; set; }
        public DbSet<Tracking> Trackings { get; set; }
        public DbSet<ChildTest> ChildTests { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOtp> UserOtp{ get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<EvaluationCard> EvaluationCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }
        }

    }



}
 
