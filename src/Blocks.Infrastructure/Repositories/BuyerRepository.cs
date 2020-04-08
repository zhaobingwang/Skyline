using Blocks.Domain.BuyerAggregate;
using Blocks.Domain.OrderAggregate;
using Blocks.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blocks.Infrastructure.Repositories
{
    public class BuyerRepository : Repository<Buyer, long, OrderingContext>
    {
        public BuyerRepository(OrderingContext dbContext) : base(dbContext)
        {
        }
    }
}
