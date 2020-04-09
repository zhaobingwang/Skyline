using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blocks.API.Application.Commands;
using Blocks.API.Application.Queries;
using Blocks.API.ViewModels;
using Blocks.Domain.OrderAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blocks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("create")]
        public async Task<long> CreateOrder([FromBody]CreateOrderCommand cmd)
        {
            return await _mediator.Send(cmd, HttpContext.RequestAborted);
        }

        [HttpGet]
        [Route("get")]
        public async Task<List<BuyerOrderViewModel>> QueryOrder([FromBody]OrderQueryViewModel vmQuery)
        {
            List<BuyerOrderViewModel> vmOrderList = new List<BuyerOrderViewModel>();
            var query = new OrderQuery(vmQuery.BuyerId);
            var orderEntities = await _mediator.Send(query);
            orderEntities.ForEach(orderEntity =>
            {
                vmOrderList.Add(new BuyerOrderViewModel
                {
                    BuyerId = orderEntity.BuyerId,
                    BuyerName = orderEntity.BuyerName,
                    ItemCount = orderEntity.ItemCount
                });
            });
            return vmOrderList;
        }
    }
}