using MediatR;
using Skyline.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Skyline.WebMvc.Commands
{
    public class ContactDeleteCommandHandler : IRequestHandler<ContactDeleteCommand, bool>
    {
        IContactRepository _contactRepository;
        public ContactDeleteCommandHandler(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public async Task<bool> Handle(ContactDeleteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _contactRepository.GetByIdAsync(request.ContactId);
            await _contactRepository.DeleteAsync(entity);
            return true;
        }
    }
}
