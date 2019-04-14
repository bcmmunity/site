using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using site.Models;

namespace site.Controllers
{
	public class HomeController : Controller
	{

		private readonly UserManager<User> _userManager;
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public ApplicationContext _db;

		public HomeController(ApplicationContext db, UserManager<User> userManager)
		{
			_userManager = userManager;
			_db = db;
		}

		public IActionResult Index()
		{
			return RedirectToAction("About");
			
		}

		public ActionResult About()
		{
			return View(_db);
		}
		
		
		public ActionResult ProjectsLoad(int offset, int count)
		{
			if (offset + count > _db.Projects.Count())
			{
				count = _db.Projects.Count() - offset;
			}

			
			return PartialView("Projects", _db.Projects.Skip(offset).Take(count).ToArray());
		}
		
		public ActionResult TeamsLoad(int offset, int count)
		{
			if (offset + count > _db.Users.Count())
			{
				count = _db.Users.Count() - offset;
			}
			return PartialView("Members", _db.Users.OrderBy(d => d.Rang).Skip(offset).Take(count).ToArray());
		}

		public ActionResult TopArticlesLoad()
		{
			return PartialView("TopArticles", _db.Articles
				.Include("Author")
				.OrderByDescending(d => d.Date)
				.Take(3)
				.ToArray());
		}

		public ActionResult ArticlesLoad(int offset, int count)
		{
			if (offset + count > _db.Users.Count())
			{
				count = _db.Users.Count() - offset;
			}

			return PartialView("Articles", _db.Articles
				.Include("Author")
				.Skip(offset)
				.Take(count)
				.ToArray());
		}
		
		public ActionResult Contacts()
		{
			return View();
		}
	}
}
