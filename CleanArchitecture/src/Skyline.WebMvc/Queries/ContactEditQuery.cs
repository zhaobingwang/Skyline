using MediatR;
using Skyline.WebMvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.WebMvc.Queries
{
    public class ContactEditQuery : IRequest<ContactEditViewModel>
    {
        public int ContactId { get; private set; }
        public ContactEditQuery(int cid)
        {
            ContactId = cid;
        }
    }
}
