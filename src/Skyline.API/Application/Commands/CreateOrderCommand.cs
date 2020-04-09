using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.API.Application.Commands
{
    public class CreateOrderCommand : IRequest<long>
    {
        public int ItemCount { get; set; }
        //public CreateOrderCommand(int itenCount) => ItemCount = itenCount;
    }
}
