using AutoMapper;
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
    public class ContactCreateHandler : BaseCommandHandler, IRequestHandler<ContactCreate, bool>
    {
        IContactRepository _contactRepository;
        public ContactCreateHandler(IContactRepository contactRepository, IMapper mapper) : base(mapper)
        {
            _contactRepository = contactRepository;
        }

        public async Task<bool> Handle(ContactCreate request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Contact>(request.ContactCreateViewModel);
            entity = await _contactRepository.AddAsync(entity);
            return entity.Id > 0;
        }
    }
}
