using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.WebMvc.Commands
{
    public class BaseCommandHandler
    {
        protected readonly IMapper _mapper;
        public BaseCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
