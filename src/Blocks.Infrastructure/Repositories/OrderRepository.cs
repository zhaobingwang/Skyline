using Blocks.Domain.OrderAggregate;
using Blocks.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Blocks.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order, long, OrderingContext>, IOrderRepository
    {
        OrderingContext _dbContext;
        public OrderRepository(OrderingContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Order>> GetOrderAsync(string buyerId)
        {
            var query = from o in _dbContext.Orders
                        where o.BuyerId == buyerId
                        select o;
            var orders = query.ToList();
            return Task.FromResult(orders);
        }
    }
}
