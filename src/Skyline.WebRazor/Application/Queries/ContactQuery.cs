using MediatR;
using Skyline.Domain.ContactAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.WebRazor.Application.Queries
{
    public class ContactListQuery : IRequest<List<Contact>>
    {
        public bool IsAuthorized { get; private set; }
        public string CurrentUserId { get; private set; }
        public ContactListQuery()
        {

        }
        public ContactListQuery(string currentUserId, bool isAuthorized)
        {
            CurrentUserId = currentUserId;
            IsAuthorized = isAuthorized;
        }
    }
}
