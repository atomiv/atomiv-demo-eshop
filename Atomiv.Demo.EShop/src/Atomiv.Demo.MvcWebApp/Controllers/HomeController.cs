﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Atomiv.Demo.MvcWebApp.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace Atomiv.Demo.MvcWebApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}


		public async Task<IActionResult> CallApi2()
		{
			var apiUrl = "https://localhost:6001/api/weatherforecast/getdata";

			var accessToken = AuthenticationHttpContextExtensions.GetTokenAsync(HttpContext, "access_token");
			var client = new HttpClient();

			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Result);

			HttpResponseMessage content = await client.GetAsync(apiUrl);
			if (content.IsSuccessStatusCode)
			{
				var json = await content.Content.ReadAsStringAsync();
				ViewData["json"] = json;
			}
			else
			{
				ViewData["json"] = "Error: " + content.StatusCode;
				
			}

			return View();
		}


		//QUICKSTART
		public async Task<IActionResult> CallApi9()
		{
			var accessToken = await HttpContext.GetTokenAsync("access_token");

			var client = new HttpClient();
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
			var content = await client.GetStringAsync("https://localhost:6001/api/weatherforecast/getdata");

			ViewBag.Json = JArray.Parse(content).ToString();
			return View("json");
		}

		public async Task<IActionResult> CallApi()
		{
			var accessToken = await HttpContext.GetTokenAsync("access_token");

			var client = new HttpClient();
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
			
			var content = await client.GetAsync("https://localhost:6001/api/weatherforecast/getdata");

			if (content.IsSuccessStatusCode)
			{
				var body = await content.Content.ReadAsStringAsync();
				var json = JArray.Parse(body).ToString();
				ViewData["json"] = json;
			}
			else
			{
				ViewData["json"] = "Error: " + content.StatusCode;

			}

			return View("json");
		}


		public IActionResult Privacy()
		{
			return View();

		}

		//public IActionResult Logout()
		//{
		//	return SignOut("Cookies", "oidc");
		//}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
