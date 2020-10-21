using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Atomiv.Demo.MvcWebApp
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();
			ConfigureIdentityServer(services);
		}

		//added
		private void ConfigureIdentityServer(IServiceCollection services)
		{
			var builder = services.AddAuthentication(options => SetAuthenticationOptions(options));

			builder.AddCookie();
			builder.AddOpenIdConnect(options => SetOpenIdConnectOptions(options));
		}

		private void SetOpenIdConnectOptions(OpenIdConnectOptions options)
		{
			options.Authority = "https://localhost:5001";
			options.ClientId = "mvc-web-app";
			options.ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";
			options.RequireHttpsMetadata = true;
			options.Scope.Add("profile");
			options.Scope.Add("openid");
			options.ResponseType = "code id_token";
			options.UsePkce = false;
			options.SaveTokens = true;
		}

		private void SetAuthenticationOptions(AuthenticationOptions options)
		{
			options.DefaultScheme = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectDefaults.AuthenticationScheme;
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
