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
    public class ContactEditHandler : IRequestHandler<ContactEdit, bool>
    {
        private readonly IContactRepository _contactRepository;
        public ContactEditHandler(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public async Task<bool> Handle(ContactEdit request, CancellationToken cancellationToken)
        {
            var entity = await _contactRepository.GetByIdAsync(request.ContactEditViewModel.Id);
            entity.Name = request.ContactEditViewModel.Name;
            entity.Province = request.ContactEditViewModel.Province;
            entity.City = request.ContactEditViewModel.City;
            entity.Address = request.ContactEditViewModel.Address;
            entity.Email = request.ContactEditViewModel.Email;
            entity.MobileNumber = request.ContactEditViewModel.MobileNumber;

            await _contactRepository.UpdateAsync(entity);
            return true;
        }
    }
}
