using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skyline.ApplicationCore.Email;
using Skyline.WebMvc.Services;

namespace Skyline.WebMvc.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService _weatherForecastService;
        private readonly IEmailSender _emailSender;
        public WeatherForecastController(IWeatherForecastService weatherForecastService, IEmailSender emailSender)
        {
            _weatherForecastService = weatherForecastService;
            _emailSender = emailSender;
        }
        [HttpGet]
        public async Task<string> Get()
        {
            await _emailSender.SendAsync(new EmailMessage("to", "subject", "content"));
            var result = await _weatherForecastService.GetWeatherForecast();
            return result;
        }
    }
}