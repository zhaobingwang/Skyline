using Skyline.Domain.BuyerAggregate;
using Skyline.Domain.OrderAggregate;
using Skyline.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Infrastructure.Repositories
{
    public class BuyerRepository : Repository<Buyer, long, OrderingContext>
    {
        public BuyerRepository(OrderingContext dbContext) : base(dbContext)
        {
        }
    }
}
