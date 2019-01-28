using Microsoft.AspNetCore.Mvc;
using site.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace site.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MainController : Controller
	{
		static ApplicationContext db;

		public static void Unit()
		{
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
			optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=usersstoredb;Trusted_Connection=True;MultipleActiveResultSets=true");
			db = new ApplicationContext(optionsBuilder.Options);
		}

		[Route("GetProjects")]
		public JsonResult GetProjects()
		{
			List<Project> projects = new List<Project>();

			projects.Add(new Project { Name = "Проект 1", Description = "Супер проект", Img = "https://www.voltmobi.com/wp-content/uploads/Untitled-5.jpg" });
			projects.Add(new Project { Name = "Проект 2", Description = "Супер проект", Img = "https://www.voltmobi.com/wp-content/uploads/Untitled-5.jpg" });
			projects.Add(new Project { Name = "Проект 3", Description = "Супер проект", Img = "https://www.voltmobi.com/wp-content/uploads/Untitled-5.jpg" });

			return Json(projects);
		}

		[Route("GetTeams")]
		public JsonResult GetTeams()
		{
			List<Team> teams = new List<Team>();

			teams.Add(new Team { Name = "Команда 1", Description = "Супер команда", Members = new List<User> { new User { UserName = "Ваня", photo = "https://www.internet-design.ru/upload/iblock/4ea/programmist.svg" } } });
			teams.Add(new Team { Name = "Команда 2", Description = "Супер команда", Members = new List<User> { new User { UserName = "Петя", photo = "https://www.internet-design.ru/upload/iblock/4ea/programmist.svg" } } });
			teams.Add(new Team { Name = "Команда 3", Description = "Супер команда", Members = new List<User> { new User { UserName = "Коля", photo = "https://www.internet-design.ru/upload/iblock/4ea/programmist.svg" } } });

			return Json(teams);
		}

		[Route("GetTeam")]
		public JsonResult GetTeam(Project p)
		{	
			return Json(p);
		}
	}
}