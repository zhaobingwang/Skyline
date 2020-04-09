using Blocks.Domain.OrderAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blocks.API.Application.Queries
{
    public class OrderQuery : IRequest<List<Order>>
    {
        public string BuyerId { get; private set; }
        public OrderQuery(string buyerId) => BuyerId = buyerId;
    }
}
