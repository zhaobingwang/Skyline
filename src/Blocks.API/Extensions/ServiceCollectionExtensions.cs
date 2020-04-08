using Blocks.Domain.OrderAggregate;
using Blocks.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blocks.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOrderingContext(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction)
        {
            return services.AddDbContext<OrderingContext>(optionsAction);
        }

        public static IServiceCollection AddSqlServerOrderingContext(this IServiceCollection services, string connectionString = "Server=.;Initial Catalog=Blocks.OrderingDb;Integrated Security=true")
        {
            return services.AddOrderingContext(builder =>
            {
                builder.UseSqlServer(connectionString);
            });
        }
        public static IServiceCollection AddMediatRServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(OrderingContextTransactionBehavior<,>));
            return services.AddMediatR(typeof(Order).Assembly, typeof(Program).Assembly);
        }
    }
}
