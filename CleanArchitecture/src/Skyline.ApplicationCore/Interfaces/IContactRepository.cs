using Skyline.ApplicationCore.Entities.ContactAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.ApplicationCore.Interfaces
{
    public interface IContactRepository : IAsyncRepository<Contact, int>
    {
    }
}
