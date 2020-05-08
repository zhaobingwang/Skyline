using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.WebMvc.Services
{
    public interface IWeatherForecastService
    {
        Task<string> GetWeatherForecast();
    }
}
