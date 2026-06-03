using Qandil.Core.Common;

namespace Qandil.Core.Entity
{
    public class School : BaseEntity
    {
        public string SchoolName { get; set; }
        public string PhoneNumber {  get; set; }
        public string Address {  get; set; }
        public string PrincipalName {  get; set; }
        public string Notes {  get; set; }
        public ICollection<Tracking> trackings { get; set; }



    }
}
