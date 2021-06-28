using Microsoft.AspNetCore.Mvc;
using SimpleCRM.Models;
using System.Diagnostics;

namespace SimpleCRM.Controllers
{
	public class HomeController : Controller
	{
		public ViewResult Index()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
