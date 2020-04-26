using MediatR;
using Skyline.ApplicationCore.Entities.ContactAggregate;
using Skyline.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Skyline.WebMvc.Commands
{
    public class ContactCreateHandler : IRequestHandler<ContactCreate, bool>
    {
        IContactRepository _contactRepository;
        public ContactCreateHandler(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public async Task<bool> Handle(ContactCreate request, CancellationToken cancellationToken)
        {
            var entity = new Contact
            {
                OwnerId = request.OwnerId,
                Name = request.ContactCreateViewModel.Name,
                Province = request.ContactCreateViewModel.Province,
                City = request.ContactCreateViewModel.City,
                Address = request.ContactCreateViewModel.Address,
                Email = request.ContactCreateViewModel.Email,
                MobileNumber = request.ContactCreateViewModel.MobileNumber,
                Status = ContactStatus.Submitted,
                Zip = "",   // No assignment for now
            };
            entity = await _contactRepository.AddAsync(entity);
            return entity.Id > 0;
        }
    }
}
