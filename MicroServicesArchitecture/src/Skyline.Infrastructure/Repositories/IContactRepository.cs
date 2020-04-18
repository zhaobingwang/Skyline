using Skyline.Domain.ContactAggregate;
using Skyline.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skyline.Infrastructure.Repositories
{
    public interface IContactRepository : IRepository<Contact, Guid>
    {
        /// <summary>
        /// 获取通讯录
        /// TODO:表达式树实现条件查询
        /// </summary>
        /// <returns></returns>
        Task<List<Contact>> GetContactsWithOutAuthorized(string currentUserId);

        Task<List<Contact>> GetAllContactsWithAuthorized();
    }
}
