using MediatR;
using Skyline.ApplicationCore.Interfaces;
using Skyline.WebMvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Skyline.WebMvc.Commands
{
    public class ContactListHandler : IRequestHandler<ContactList, IEnumerable<ContactViewModel>>
    {
        private readonly IContactRepository _contactRepository;
        public ContactListHandler(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public async Task<IEnumerable<ContactViewModel>> Handle(ContactList request, CancellationToken cancellationToken)
        {
            var contacts = await _contactRepository.ListAllAsync();
            return contacts.Select(c => new ContactViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                Province = c.Province,
                City = c.City,
                Email = c.Email,
                MobileNumber = c.MobileNumber,
                Status = c.Status
            });
        }
    }
}
