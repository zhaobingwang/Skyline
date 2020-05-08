using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Skyline.ApplicationCore.Interfaces;
using Skyline.ApplicationCore.Specifications;
using Skyline.Infrastructure.Identity;
using Skyline.WebMvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Skyline.WebMvc.Queries
{
    public class ContactListQueryHandler : BaseQueryHandler, IRequestHandler<ContactListQuery, IEnumerable<ContactViewModel>>
    {
        private readonly IContactRepository _contactRepository;
        public ContactListQueryHandler(IContactRepository contactRepository, IMapper mapper) : base(mapper)
        {
            _contactRepository = contactRepository;
        }
        public async Task<IEnumerable<ContactViewModel>> Handle(ContactListQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyList<ApplicationCore.Entities.ContactAggregate.Contact> contacts;

            // 只能查看审核通过的通讯录，除非被授权或是该通讯录的创建者
            if (!request.IsAuthorized)
            {
                ContacListUnAuthorizedSpecification unAuthorizedSpec = new ContacListUnAuthorizedSpecification(request.UserId);
                contacts = await _contactRepository.ListAsync(unAuthorizedSpec);
            }
            else
            {
                contacts = await _contactRepository.ListAllAsync();
            }
            return _mapper.Map<IEnumerable<ContactViewModel>>(contacts);
        }
    }
}
