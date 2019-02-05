using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using site.Models;

namespace site.Controllers
{
	public class HomeController : Controller
	{
		

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}


		public ActionResult Index()
		{
			int offset = 0;
			int count = 10;
			Project[] projects = MainController.db.Projects
				.Skip(offset)
				.Take(count)
				.ToArray();
			ViewBag.p = projects;
			return View();
		}
	}
}
