using MediatR;
using Skyline.ApplicationCore.Interfaces;
using Skyline.WebMvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Skyline.WebMvc.Queries
{
    public class ContactEditQueryHandler : IRequestHandler<ContactEditQuery, ContactEditViewModel>
    {
        private readonly IContactRepository _contactRepository;
        public ContactEditQueryHandler(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public async Task<ContactEditViewModel> Handle(ContactEditQuery request, CancellationToken cancellationToken)
        {
            var entity = await _contactRepository.GetByIdAsync(request.ContactId);
            return new ContactEditViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                OwnerId = entity.OwnerId,
                Province = entity.Province,
                City = entity.City,
                Address = entity.Address,
                Email = entity.Email,
                MobileNumber = entity.MobileNumber
            };
        }
    }
}
