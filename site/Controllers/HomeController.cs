using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

//		private string _bd = "Server=localhost\\SQLEXPRESS;Database=a23;Trusted_Connection=True;";
		private string _bd = "Server=(localdb)\\mssqllocaldb;Database=q112;Trusted_Connection=True;";

		public ActionResult Index()
		{
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
//			optionsBuilder.UseSqlServer("Server=localhost;Database=u0641156_diffind;User Id = u0641156_diffind; Password = Qwartet123!");
//			optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=q112;Trusted_Connection=True;");
			optionsBuilder.UseSqlServer(_bd);
			var db = new ApplicationContext(optionsBuilder.Options);

			return View(db);
		}

		public ActionResult About()
		{
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
//			optionsBuilder.UseSqlServer("Server=localhost;Database=u0641156_diffind;User Id = u0641156_diffind; Password = Qwartet123!");
//			optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=q112;Trusted_Connection=True;");
			optionsBuilder.UseSqlServer(_bd);
			var db = new ApplicationContext(optionsBuilder.Options);

			return View(db);

			
		}
		
		
		public ActionResult ProjectsLoad(int offset, int count)
		{
			if (offset + count > MainController.db.Projects.Count())
			{
				count =	MainController.db.Projects.Count() - offset;
			}

			
			return PartialView("Projects", MainController.db.Projects.Skip(offset).Take(count).ToArray());
		}
		
		public ActionResult TeamsLoad(int offset, int count)
		{
			if (offset + count > MainController.db.Users.Count())
			{
				count =	MainController.db.Users.Count() - offset;
			}
			return PartialView("Members", MainController.db.Users.OrderBy(d => d.Rang).Skip(offset).Take(count).ToArray());
		}

		public ActionResult TopArticlesLoad()
		{
			return PartialView("TopArticles", MainController.db.Articles
				.Include("Author")
				.OrderByDescending(d => d.Date)
				.Take(3)
				.ToArray());
		}

		public ActionResult ArticlesLoad(int offset, int count)
		{
			if (offset + count > MainController.db.Users.Count())
			{
				count =	MainController.db.Users.Count() - offset;
			}

			return PartialView("Articles", MainController.db.Articles
				.Include("Author")
				.Skip(offset)
				.Take(count)
				.ToArray());
		}

		public ActionResult Projects(int id)
		{
			Project proj = MainController.db.Projects.Find(id);
			if (proj != null)
			{
				return View("Project", proj);	
			}
			else
			{
				return View("Error");
			}
			
		}

		public ActionResult Profile(string id)
		{
			User user = MainController.db.Users.Find(id);
			if (user != null)
			{
				return View("Profile", user);
			}
			else
			{
				return View("Error");
			}
		}
		
		public ActionResult Contacts()
		{
			return View();
		}
	}
}
