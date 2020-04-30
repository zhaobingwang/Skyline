using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.WebMvc
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var configuration = new MapperConfiguration(config =>
            {
                config.AddProfile<MappingProfile>();
            });
            IMapper mapper = configuration.CreateMapper();
            return services.AddSingleton(mapper);
        }
    }
}
