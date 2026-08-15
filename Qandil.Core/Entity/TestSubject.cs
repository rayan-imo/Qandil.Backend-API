namespace Qandil.Core.Entity
{
   
        public class TestSubject
        {
            public Guid TestId{ get; set; }
            public Test Test { get; set; }
            public Guid SubjectId { get; set; }
            public Subject Subject { get; set; }
            public double MaxMark {  get; set; }

        }
}
