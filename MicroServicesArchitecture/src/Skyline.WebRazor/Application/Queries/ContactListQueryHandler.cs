using MediatR;
using Skyline.Domain.ContactAggregate;
using Skyline.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Skyline.WebRazor.Application.Queries
{
    public class ContactListQueryHandler : IRequestHandler<ContactListQuery, List<Contact>>
    {
        IContactRepository _contactRepository;
        public ContactListQueryHandler(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public async Task<List<Contact>> Handle(ContactListQuery request, CancellationToken cancellationToken)
        {
            // 只能查看审核通过的通讯录，除非被授权或是该通讯录的创建者
            if (!request.IsAuthorized)
            {
                return await _contactRepository.GetContactsWithOutAuthorized(request.CurrentUserId);
            }
            else
            {
                return await _contactRepository.GetAllContactsWithAuthorized();
            }
        }
    }
}
