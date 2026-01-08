using Microsoft.AspNetCore.Mvc;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService weatherForecastService)
        {
            _logger = logger;
            _weatherForecastService = weatherForecastService;
        }

        [HttpPost("generate")]
        public IActionResult Post([FromQuery] int count, [FromBody] Temperature temperature)
        {
            if (count < 0 || temperature.Max < temperature.Min)
            {
                return BadRequest("Need to check values");
            }
            var result = _weatherForecastService.Get(count, temperature.Min, temperature.Max);
            return Ok(result);
        }

        //[HttpGet("currentDay")]
        //public WeatherForecast GetCurrentDayForecast()
        //{
        //    var result = _weatherForecastService.Get().First();
        //    return result;
        //}
        //[HttpPost]
        //public string Hello([FromBody] string name)
        //{
        //    return $"Hello, {name}!";
        //}
    }
}
