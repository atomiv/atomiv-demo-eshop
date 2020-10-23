using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Atomiv.Demo.Ordering.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	//[Authorize]
	public class WeatherForecastController : ControllerBase
	{
		// MUNAGA
		// GET api/weatherforecast
		[HttpGet]
		[Route("getdata2")]
		//[Authorize]
		public ActionResult Get2()
		{
			var claims = HttpContext.User.Claims.Select(x => $"{x.Type}:{x.Value}");
			return Ok(new
			{
				Name = "Values API - jeca",
				Claims = claims.ToArray()
			});
		}

		//QUICKSTART
		[HttpGet]
		[Route("getdata")]
		//[Authorize]
		public IActionResult Get()
		{
			return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
		}


		//private static readonly string[] Summaries = new[]
		//{
		//	"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		//};

		//private readonly ILogger<WeatherForecastController> _logger;

		//public WeatherForecastController(ILogger<WeatherForecastController> logger)
		//{
		//	_logger = logger;
		//}

		//[HttpGet]
		//public IEnumerable<WeatherForecast> Get()
		//{
		//	var rng = new Random();
		//	return Enumerable.Range(1, 5).Select(index => new WeatherForecast
		//	{
		//		Date = DateTime.Now.AddDays(index),
		//		TemperatureC = rng.Next(-20, 55),
		//		Summary = Summaries[rng.Next(Summaries.Length)]
		//	})
		//	.ToArray();
		//}
	}
}
