using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Atomiv.Demo.MvcWebApp.Controllers
{
	public class AccountController : Controller
	{
		// GET: Login
		public ActionResult Login()
		{
			return Challenge(new AuthenticationProperties() { RedirectUri = "Home/Index" },
				"oidc");
		}

		public ActionResult Logout()
		{
			return SignOut(new AuthenticationProperties() { RedirectUri = "Home/Index" },
				"Cookies", "oidc");

		}


	}
}
