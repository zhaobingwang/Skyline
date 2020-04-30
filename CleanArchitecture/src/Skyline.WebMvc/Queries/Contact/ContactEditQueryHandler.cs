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
    public class ContactEditQueryHandler : BaseQueryHandler, IRequestHandler<ContactEditQuery, ContactEditViewModel>
    {
        private readonly IContactRepository _contactRepository;
        public ContactEditQueryHandler(IContactRepository contactRepository, IMapper mapper) : base(mapper)
        {
            _contactRepository = contactRepository;
        }

        public async Task<ContactEditViewModel> Handle(ContactEditQuery request, CancellationToken cancellationToken)
        {
            var entity = await _contactRepository.GetByIdAsync(request.ContactId);
            return _mapper.Map<ContactEditViewModel>(entity);
        }
    }
}
