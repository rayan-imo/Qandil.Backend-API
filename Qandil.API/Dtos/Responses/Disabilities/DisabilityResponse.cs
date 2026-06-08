using Qandil.Core.Entity;

namespace Qandil.API.Dtos.Responses.Disabilities
{
    public class DisabilityResponse
    {
        public string Name { get; set; }

        public static DisabilityResponse Transform (Disability disability)
        {
            return new DisabilityResponse
            {
                Name = disability.Name,
            };
        }
    }
}
