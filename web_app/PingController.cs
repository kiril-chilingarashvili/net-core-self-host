using System;
using Microsoft.AspNetCore.Mvc;

namespace web_app
{
	public class PingController : Controller
	{
		[HttpGet("")]
		public string Ping()
		{
			return "ok";
		}
	}
}
