using Qandil.Core.Entity;

namespace Qandil.API.Dtos.Responses.Subjects
{
    public class SubjectResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public static SubjectResponse Transform(Subject subject)
        {
            return new SubjectResponse
            {
                Id = subject.Id,
                Name = subject.Name
            };
        }
    }
}
