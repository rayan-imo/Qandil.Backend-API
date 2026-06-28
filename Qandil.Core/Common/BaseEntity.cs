namespace Qandil.Core.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
       
    }
}
