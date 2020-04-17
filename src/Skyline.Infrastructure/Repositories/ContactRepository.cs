using Microsoft.EntityFrameworkCore;
using Skyline.Domain.ContactAggregate;
using Skyline.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyline.Infrastructure.Repositories
{
    public class ContactRepository : Repository<Contact, Guid, ApplicationDbContext>, IContactRepository
    {
        ApplicationDbContext _dbContext;
        public ContactRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Contact>> GetAllContactsWithAuthorized()
        {
            var contacts = await _dbContext.Contacts.ToListAsync();
            return contacts;
        }

        /// <summary>
        /// 获取通讯录
        /// TODO:表达式树实现条件查询
        /// </summary>
        /// <returns></returns>
        public async Task<List<Contact>> GetContactsWithOutAuthorized(string currentUserId)
        {
            var contacts = _dbContext.Contacts.Where(c => c.Status == ContactStatus.Approved
                 || c.OwnerId == currentUserId);
            return await contacts.ToListAsync();
        }
    }
}
