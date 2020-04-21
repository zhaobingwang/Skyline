using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Skyline.Utilities.UnitTests
{
    [Trait("工具类", "Json")]
    public class JsonUtilTest
    {
        [Fact(DisplayName = "Json转对象-正常入参")]
        public void ToObject_ShouldSuccess_WithExpectedParameters()
        {
            var json = "{\"Location\":\"Hangzhou\",\"Temperature\":4,\"Date\":\"2020-02-25T21:03:37+08:00\"}";
            var result = JsonUtil.ToObject<WeatherForecast>(json);

            Assert.True(result.Location == "Hangzhou");
            Assert.True(result.Temperature == 4);
            Assert.IsType<DateTimeOffset>(result.Date);
            Assert.True(result.Date == new DateTimeOffset(2020, 2, 25, 21, 03, 37, TimeSpan.FromHours(8)));
        }

        [Fact(DisplayName = "Json转对象-空值入参")]
        public void ToObject_ReturnDefault_WithNull()
        {
            string json = "";
            var result = JsonUtil.ToObject<WeatherForecast>(json);

            Assert.True(result == default(WeatherForecast));
        }

        [Fact(DisplayName = "对象转Json-正常入参")]
        public void ToJson_ShouldSuccess_WithExpectedParameters()
        {
            var weatherForecast = new WeatherForecast
            {
                Location = "Hangzhou",
                Temperature = 4,
                Date = new DateTimeOffset(2020, 2, 25, 21, 03, 37, TimeSpan.FromHours(8))
            };
            var expected = new StringBuilder();
            expected.Append("{");
            expected.Append("\"Location\":\"Hangzhou\",");
            expected.Append("\"Temperature\":4,");
            expected.Append("\"Date\":\"2020-02-25T21:03:37+08:00\"");
            expected.Append("}");

            var result = JsonUtil.ToJson(weatherForecast);
            Assert.Equal(expected.ToString(), result);
        }

        class WeatherForecast
        {
            public string Location { get; set; }
            public int Temperature { get; set; }
            public DateTimeOffset Date { get; set; }
        }

    }
}
