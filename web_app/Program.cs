using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

class Program
{
	private static string mPort = "8080";
	static void Main(string[] args)
	{
		var url = "http://*:" + mPort;

		var startup = new Startup(url);

		var host = new WebHostBuilder()
			.UseKestrel()
			.UseUrls(url)
			.ConfigureServices(startup.ConfigureServices)
			.Configure(startup.Configure)
			.Build();

		host.Start();

		Console.ReadLine();
	}

	public class Startup
	{
		private readonly string mUrl;

		public Startup(
			string url)
		{
			mUrl = url;
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseCors(policy =>
			{
				policy.AllowAnyOrigin();
				policy.AllowAnyMethod();
				policy.AllowAnyHeader();
			});

			// Add the Web API framework to the app's pipeline
			app.UseMvc();
		}

		public void ConfigureServices(IServiceCollection services)
		{
			 services.AddMvc().AddJsonOptions(opt =>
			 {
			    opt.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
			    opt.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;

			    opt.SerializerSettings.DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate;
			    opt.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter { CamelCaseText = true });
			 });
			// Add Cors support to the service
			services.AddScoped(_ => System.Text.Encodings.Web.UrlEncoder.Default);
			services.AddCors();
		}
	}
}
