﻿using System;
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


		public ActionResult Index()
		{
			User kek = new User();
		
			int offset = 0;
			int count = 10;
			Project[] projects = MainController.db.Projects
				.Skip(offset)
				.Take(count)
				.ToArray();
			ViewBag.p = projects;
			return View();
		}

		public ActionResult About()
		{
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
			optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=q12;Trusted_Connection=True;");
//			optionsBuilder.UseSqlServer(
//				"Server=localhost\\SQLEXPRESS;Database=t49;Trusted_Connection=True;");
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
			return PartialView("Members", MainController.db.Users.Skip(offset).Take(count).ToArray());
		}
		
		
		public ActionResult Contacts()
		{
			return View();
		}
	}
}
