using MediatR;
using Skyline.WebMvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.WebMvc.Queries
{
    public class ContactDetailsQuery : IRequest<ContactDetailsViewModel>
    {
        public int ContactId { get; private set; }
        public ContactDetailsQuery(int cid)
        {
            ContactId = cid;
        }
    }
}
