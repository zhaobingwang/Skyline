using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Skyline.Elasticsearch.Sample.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ESConfig>(options =>
            {
                options.Urls = Configuration.GetSection("ES:ConnectionStrings").GetChildren().ToList().Select(x => x.Value).ToList();
                options.UserName = Configuration.GetSection("ES:UserName").Value;
                options.Password = Configuration.GetSection("ES:Password").Value;
            });
            services.AddSingleton<IESClientProvider, ESClientProvider>();
            services.AddTransient<ExceptionLogContext>();
            //var types = Assembly.Load("Skyline.Elasticsearch.Sample.WebApp").GetTypes().Where(p => !p.IsAbstract && (p.GetInterfaces().Any(i => i == typeof(IESContext)))).ToList();
            //types.ForEach(p =>
            //        services.AddTransient(p)
            //);
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
