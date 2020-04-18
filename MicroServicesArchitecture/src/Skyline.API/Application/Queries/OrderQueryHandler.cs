using Skyline.Domain.OrderAggregate;
using Skyline.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Skyline.API.Application.Queries
{
    public class OrderQueryHandler : IRequestHandler<OrderQuery, List<Order>>
    {
        IOrderRepository _orderRepository;
        public OrderQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<List<Order>> Handle(OrderQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrderAsync(request.BuyerId);
            return orders;
        }
    }
}
