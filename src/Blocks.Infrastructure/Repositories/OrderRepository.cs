using Blocks.Domain.OrderAggregate;
using Blocks.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blocks.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order, long, OrderingContext>, IOrderRepository
    {
        public OrderRepository(OrderingContext dbContext) : base(dbContext)
        {
        }
    }
}
