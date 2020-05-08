using AutoMapper;
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
    public class ContactDetailsQueryHandler : BaseQueryHandler, IRequestHandler<ContactDetailsQuery, ContactDetailsViewModel>
    {
        private readonly IContactRepository _contactRepository;
        public ContactDetailsQueryHandler(IContactRepository contactRepository, IMapper mapper) : base(mapper)
        {
            _contactRepository = contactRepository;
        }

        public async Task<ContactDetailsViewModel> Handle(ContactDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _contactRepository.GetByIdAsync(request.ContactId);
            return _mapper.Map<ContactDetailsViewModel>(entity);
        }
    }
}
