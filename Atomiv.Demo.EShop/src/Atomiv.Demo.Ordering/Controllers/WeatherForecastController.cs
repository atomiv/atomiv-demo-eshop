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
		public IActionResult Get()
		{
			return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
		}


	}
}
