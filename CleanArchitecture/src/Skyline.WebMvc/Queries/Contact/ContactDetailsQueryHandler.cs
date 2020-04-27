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
    public class ContactDetailsQueryHandler : IRequestHandler<ContactDetailsQuery, ContactDetailsViewModel>
    {
        private readonly IContactRepository _contactRepository;
        public ContactDetailsQueryHandler(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public async Task<ContactDetailsViewModel> Handle(ContactDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _contactRepository.GetByIdAsync(request.ContactId);
            return new ContactDetailsViewModel
            {
                Id = entity.Id,
                OwnerId = entity.OwnerId,
                Name = entity.Name,
                Province = entity.Province,
                City = entity.City,
                Address = entity.Address,
                Email = entity.Email,
                MobileNumber = entity.MobileNumber,
                Zip = entity.Zip,
                Status = entity.Status
            };
        }
    }
}
