using MediatR;
using Skyline.WebMvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.WebMvc.Commands
{
    public class ContactDeleteCommand : IRequest<bool>
    {
        public int ContactId { get; set; }
        public ContactDeleteCommand(int cid)
        {
            ContactId = cid;
        }
    }
}
