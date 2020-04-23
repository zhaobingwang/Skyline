using Skyline.ApplicationCore.Interfaces;

namespace Skyline.ApplicationCore.Entities.ContactAggregate
{
    public class Contact : BaseEntity<int>, IAggregateRoot
    {
        public string OwnerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public ContactStatus Status { get; set; }
    }
}
