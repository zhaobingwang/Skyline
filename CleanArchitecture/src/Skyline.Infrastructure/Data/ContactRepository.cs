using Skyline.ApplicationCore.Entities.ContactAggregate;
using Skyline.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Infrastructure.Data
{
    public class ContactRepository : EFRepository<Contact, int>, IContactRepository
    {
        public ContactRepository(SkylineDbContext dbContext) : base(dbContext)
        {
        }
    }
}
