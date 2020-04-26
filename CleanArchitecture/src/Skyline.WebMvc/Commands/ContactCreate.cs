using MediatR;
using Skyline.WebMvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.WebMvc.Commands
{
    public class ContactCreate : IRequest<bool>
    {
        public ContactCreateViewModel ContactCreateViewModel { get; private set; }
        public string OwnerId { get; private set; }
        public ContactCreate(string ownerId, ContactCreateViewModel vm)
        {
            ContactCreateViewModel = vm;
            OwnerId = ownerId;
        }
    }
}
