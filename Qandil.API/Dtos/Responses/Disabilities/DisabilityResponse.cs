using Qandil.Core.Entity;

namespace Qandil.API.Dtos.Responses.Disabilities
{
    public class DisabilityResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public static DisabilityResponse Transform (Disability disability)
        {
            return new DisabilityResponse
            {  Id = disability.Id,
                Name = disability.Name,
            };
        }
    }
}
