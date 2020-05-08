using MediatR;
using Skyline.WebMvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.WebMvc.Commands
{
    public class ContactEditStatusCommand : IRequest<bool>
    {
        public ContactEditStatusViewModel EditStatusViewModel { get; set; }
        public ContactEditStatusCommand(ContactEditStatusViewModel vm)
        {
            EditStatusViewModel = vm;
        }
    }
}
