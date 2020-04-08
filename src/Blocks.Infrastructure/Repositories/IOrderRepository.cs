using Blocks.Domain.OrderAggregate;
using Blocks.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blocks.Infrastructure.Repositories
{
    public interface IOrderRepository : IRepository<Order, long>
    {
    }
}
