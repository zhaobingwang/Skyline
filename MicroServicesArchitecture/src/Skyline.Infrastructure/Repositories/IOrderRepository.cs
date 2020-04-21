using Skyline.Domain.OrderAggregate;
using Skyline.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skyline.Infrastructure.Repositories
{
    public interface IOrderRepository : IRepository<Order, long>
    {
        Task<List<Order>> GetOrderAsync(string buyerId);
    }
}
