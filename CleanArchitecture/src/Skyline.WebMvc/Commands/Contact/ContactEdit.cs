using MediatR;
using Skyline.WebMvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.WebMvc.Commands
{
    public class ContactEdit : IRequest<bool>
    {
        public ContactEditViewModel ContactEditViewModel { get; set; }
        public ContactEdit(ContactEditViewModel vm)
        {
            ContactEditViewModel = vm;
        }
    }
}
