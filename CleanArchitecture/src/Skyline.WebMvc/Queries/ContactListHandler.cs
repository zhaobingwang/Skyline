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
    public class ContactListHandler : IRequestHandler<ContactList, IEnumerable<ContactViewModel>>
    {
        private readonly IContactRepository _contactRepository;
        public ContactListHandler(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public async Task<IEnumerable<ContactViewModel>> Handle(ContactList request, CancellationToken cancellationToken)
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

            return contacts.Select(c => new ContactViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                Province = c.Province,
                City = c.City,
                Email = c.Email,
                MobileNumber = c.MobileNumber,
                Status = c.Status
            });
        }
    }
}
