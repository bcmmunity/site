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
			optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=test79;Trusted_Connection=True;");
			db = new ApplicationContext(optionsBuilder.Options);
//			TestAdd();
		}


		private static void TestAdd()
		{
			
			string[] names = { "kak pisat bota", "devblog 1", "dogovor", "1000000 userov", "spulat mulae" };
			string body =
				"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
			string descr = body.Substring(0, 50);
			Tag t1 = new Tag {Name = "C#"};
			Tag t2 = new Tag {Name = "Java"};
			List<Tag> tags = new List<Tag>();
			tags.Add(t1);
			tags.Add(t2);
			
			User auth = new User();
			
			for (int i = 0; i < 5; i++)
			{
				Console.WriteLine("asdasdd");
				Article temp = new Article
				{
					Name = names[i],
					Body = body,
					Description = descr,
					Author = auth,
					Date = new DateTime(2019, 12, i + 1),
					Tags = tags
				};
				
				
				db.Articles.Add(temp);
				db.SaveChanges();
			}

			
		}
		
		[Route("getArticles")]
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


		[Route("getTopArticles")]
		[HttpGet]
		public JsonResult GetTopArticles()
		{
			var temp = new
			{
				kek = new string[] {"тут будут топовые статьи"}
			};

			return Json(temp);
		}

		[Route("getArticle")] 
		[HttpGet]
		public JsonResult GetArticle(int? id)
		{

			Console.WriteLine(id);
			Console.WriteLine("\n\n\n");
			Article art = db.Articles.FirstOrDefault(a => a.ArticleId == id) ;
			var temp = new
			{
				article = art
			};
			return Json(temp);
		}

		[Route("getTeams")]
		[HttpGet]
		public JsonResult GetTeams()
		{
			Article art = db.Articles.FirstOrDefault(a => a.ArticleId == 0);
			var temp = new
			{
				k = "k",
				art
			};
			return Json(temp);
		}

		[Route("getTeamInfo")]
		[HttpGet]
		public JsonResult GetTeamInfo(int? id)
		{
			var temp = new
			{
				teamId = id
			};
			return Json(temp);
		}

		[Route("getMembers")]
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

		[Route("getMembers")]
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
		
		[Route("getMemberInfo")]
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
		
		[Route("getProjects")]
		public JsonResult GetProjects()
		{
			List<Project> projects = new List<Project>();

			projects.Add(new Project { Name = "Проект 1", Description = "Супер проект", Img = "https://www.voltmobi.com/wp-content/uploads/Untitled-5.jpg" });
			projects.Add(new Project { Name = "Проект 2", Description = "Супер проект", Img = "https://www.voltmobi.com/wp-content/uploads/Untitled-5.jpg" });
			projects.Add(new Project { Name = "Проект 3", Description = "Супер проект", Img = "https://www.voltmobi.com/wp-content/uploads/Untitled-5.jpg" });

			return Json(projects);
		}

	

		[Route("getTeam")]
		public JsonResult GetTeam(Project p)
		{	
			return Json(p);
		}
		
		
	}
}
