using System;
using Microsoft.AspNetCore.Mvc;
using site.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
		
		[Route("GetArticles")]
		[HttpGet]
		public JsonResult GetArticles(int? skip, int? limit)
		{
			var temp = new
			{
				skip,
				limit
			};
			return Json(temp);

		}


		[Route("GetTopArticles")]
		[HttpGet]
		public JsonResult GetTopArticles()
		{
			var temp = new
			{
				kek = new string[] {"тут будут топовые статьи"}
			};

			return Json(temp);
		}

		[Route("GetArticle")]
		[HttpGet]
		public JsonResult GetArticle(int? id)
		{
			var temp = new
			{
				id
			};
			return Json(temp);
		}

		[Route("GetTeams")]
		[HttpGet]
		public JsonResult GetTeams()
		{
			var temp = new
			{
				k = "k"
			};
			return Json(temp);
		}

		[Route("GetTeamInfo")]
		[HttpGet]
		public JsonResult GetTeamInfo(int? id)
		{
			var temp = new
			{
				teamId = id
			};
			return Json(temp);
		}

		[Route("GetMembers")]
		[HttpGet]
		public JsonResult GetMembers(string id)
		{
			var temp = new
			{
				hui = "hui"
			};
			if (id != null)
			{
				if (id.ToLower() == "all")
					return Json(temp);
				if (id.All(char.IsDigit))
				{
					int _id = Convert.ToInt32(id);
					return Json(temp);
				}
			}
			throw new NullReferenceException();
		}

		[Route("GetMembers")]
		[HttpGet]
		public JsonResult GetMembers(string id, int? offset, int? count)
		{
			var temp = new
			{
				offset,
				count
			};
			return Json(temp);
		}
		
		[Route("GetMemberInfo")]
		[HttpGet]
		public JsonResult GetMemberInfo(string id)
		{
			if (id.All(char.IsDigit))
			{
				int _id = Convert.ToInt32(id);
			}
			var temp = new
			{
				id
			};
			return Json(temp);
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

	

		[Route("GetTeam")]
		public JsonResult GetTeam(Project p)
		{	
			return Json(p);
		}
		
		
	}
}
