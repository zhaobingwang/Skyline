using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Skyline.Infrastructure.Data;
using Skyline.WebMvc.Authorization;
using Skyline.WebMvc.HttpPolicies;
using Skyline.WebMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.WebMvc
{
    /// <summary>
    /// DI容器 服务注入 扩展
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加AutoMapper
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var configuration = new MapperConfiguration(config =>
            {
                config.AddProfile<MappingProfile>();
            });
            IMapper mapper = configuration.CreateMapper();
            return services.AddSingleton(mapper);
        }

        /// <summary>
        /// 添加HttpClientFactory
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            // HttpClient.
            services.AddHttpClient<IWeatherForecastService, WeatherForecastService>(client =>
            {
                client.BaseAddress = new Uri(configuration["API:Skyline:BaseUrl"]);
            })
                .SetHandlerLifetime(TimeSpan.FromMinutes(2))  // default value is 2 minutes.
                .AddPolicyHandler(GetRetryPolicy.DefaultGetRetry());

            //services.AddHttpClient("client.default",client=> {
            //    client.BaseAddress = new Uri(Configuration["API.Skyline.BaseUrl"]);
            //})
            //    .SetHandlerLifetime(TimeSpan.FromMinutes(2))    // default value is 2 minutes.
            //    .AddPolicyHandler(GetRetryPolicy());

            return services;
        }

        /// <summary>
        /// 添加SQL Server EF上下文
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddSqlServerDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SkylineDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SkylineDbContextConnection"));
            });
            return services;
        }

        /// <summary>
        /// 授权配置
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAuthorizationHandlers(this IServiceCollection services)
        {
            // Authorization handlers.
            services.AddScoped<IAuthorizationHandler, ContactIsOwnerAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ContactAdministratorsAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ContactManagerAuthorizationHandler>();
            return services;
        }
    }
}
