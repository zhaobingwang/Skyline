using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Domain.ContactAggregate
{
    public class Contact : Entity<Guid>, IAggregateRoot
    {
        public string OwnerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Email { get; set; }
        public ContactStatus Status { get; set; }
    }

    public enum ContactStatus
    {
        None,
        Submitted,
        Approved,
        Rejected
    }
}
