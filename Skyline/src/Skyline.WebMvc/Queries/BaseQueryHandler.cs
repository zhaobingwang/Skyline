using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.WebMvc.Queries
{
    public class BaseQueryHandler
    {
        protected readonly IMapper _mapper;
        public BaseQueryHandler(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
