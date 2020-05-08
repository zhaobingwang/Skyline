using MediatR;
using Skyline.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Skyline.WebMvc.Commands
{
    public class ContactEditStatusCommandHandler : IRequestHandler<ContactEditStatusCommand, bool>
    {
        private readonly IContactRepository _contactRepository;
        public ContactEditStatusCommandHandler(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public async Task<bool> Handle(ContactEditStatusCommand request, CancellationToken cancellationToken)
        {
            var entity = await _contactRepository.GetByIdAsync(request.EditStatusViewModel.Id);
            entity.Status = request.EditStatusViewModel.Status;
            await _contactRepository.UpdateAsync(entity);
            return true;
        }
    }
}
