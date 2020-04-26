using MediatR;
using Skyline.WebMvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.WebMvc.Queries
{
    public class ContactList : IRequest<IEnumerable<ContactViewModel>>
    {
        public string UserId { get; private set; }
        public bool IsAuthorized { get; private set; }
        public ContactList(string uid, bool isAuthorized)
        {
            UserId = uid;
            IsAuthorized = isAuthorized;
        }
    }
}
