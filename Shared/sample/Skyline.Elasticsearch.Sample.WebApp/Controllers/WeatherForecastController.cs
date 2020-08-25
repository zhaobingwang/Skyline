using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Skyline.Elasticsearch.Sample.WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ExceptionLogContext _exceptionLogContext;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, ExceptionLogContext exceptionLogContext)
        {
            _logger = logger;
            _exceptionLogContext = exceptionLogContext;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            try
            {
                throw new DivideByZeroException();
            }
            catch (Exception ex)
            {
                await _exceptionLogContext.InsertOne(new ExceptionLogModel
                {
                    Code = "A10001",
                    ExceptionMessage = ex.Message,
                    ExceptionDetails = ex.ToString(),
                    LogTime = DateTime.UtcNow
                });

                await _exceptionLogContext.InsertMany(new List<ExceptionLogModel> {
                    new ExceptionLogModel{
                        Code = "A10002",
                        ExceptionMessage = ex.Message,
                        ExceptionDetails = ex.ToString(),
                        LogTime = DateTime.UtcNow
                    },
                    new ExceptionLogModel{
                        Code = "A10002",
                        ExceptionMessage = ex.Message,
                        ExceptionDetails = ex.ToString(),
                        LogTime = DateTime.UtcNow
                    },
                });
                return ex.Message;
            }
            //var rng = new Random();
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[rng.Next(Summaries.Length)]
            //})
            //.ToArray();
        }
    }
}
