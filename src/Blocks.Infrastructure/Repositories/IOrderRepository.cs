using Blocks.Domain.OrderAggregate;
using Blocks.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blocks.Infrastructure.Repositories
{
    public interface IOrderRepository : IRepository<Order, long>
    {
        Task<List<Order>> GetOrderAsync(string buyerId);
    }
}
