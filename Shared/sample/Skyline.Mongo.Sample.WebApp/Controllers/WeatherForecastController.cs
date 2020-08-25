using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization.Attributes;

namespace Skyline.Mongo.Sample.WebApp.Controllers
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
        private readonly MongoRepository _mongoRepository;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, MongoRepository mongoRepository)
        {
            _logger = logger;
            _mongoRepository = mongoRepository;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            try
            {
                int a = 0;
                int b = 10 / a;
            }
            catch (Exception ex)
            {
                LogEntity logEntity = new LogEntity
                {
                    Type = ex.GetType().Name,
                    Message = ex.Message + " from web api",
                    Details = ex.ToString(),
                    LogTime = DateTime.UtcNow
                };
                _mongoRepository.InsertOne("log.exceptions", logEntity);
            }
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
    class LogEntity : MongoEntity
    {
        [BsonElement("type")]
        public string Type { get; set; }
        [BsonElement("message")]
        public string Message { get; set; }
        [BsonElement("details")]
        public string Details { get; set; }
        [BsonElement("logTime")]
        public DateTime LogTime { get; set; }
    }

}
