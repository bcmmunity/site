using System;
using Microsoft.AspNetCore.Mvc;
using site.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.CodeAnalysis;
using Project = site.Models.Project;

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
			optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=t2;Trusted_Connection=True;");
			db = new ApplicationContext(optionsBuilder.Options);
//			TestAdd();
//			TestAdd2();
//			TestAdd3();
//			TestAdd5();
		}

		#region Тестовые данные
		private static void TestAdd2()
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
			
			User auth = new User
			{
				Name = "artem",
				Surname = "kim"
			};
			
			for (int i = 0; i < 20; i++)
			{
				Console.WriteLine("asdasdd");
				Article temp = new Article
				{
					Name = names[i % 5],
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

		private static List<User> TestAdd()
		{
			string[] names = { "kak pisat bota", "devblog 1", "dogovor", "1000000 userov", "spulat mulae" };
			string url = "https://loremflickr.com/320/245";
			string surname = "deffind";
			string[] pos = {"teamlad", "jun", "senior", "middle", "gen dir"};
			Speciality sp1 = new Speciality
			{
				Name = "c#"
			};
			Speciality sp2 = new Speciality
			{
				Name = "front"
			};
			List<Speciality> spec = new List<Speciality>();
			spec.Add(sp1);
			spec.Add(sp2);
			string body =
				"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
			string descr = body.Substring(0, 50);
			List<User> kek = new List<User>();
			for (int i = 0; i < 5; i++)
			{
				Models.User temp = new User
				{
					Photo = url,
					Name = names[i % 5],
					Surname = surname,
					Position = pos[i % 5],
					Description = descr,
					Specialities = spec
				};
				kek.Add(temp);
			}

			return kek;
		}

		private static void TestAdd3()
		{
			string[] names = { "kak pisat bota", "devblog 1", "dogovor", "1000000 userov", "spulat mulae" };
			string url = "https://loremflickr.com/320/245";
			string body =
				"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
			string descr = body.Substring(0, 50);
			Speciality sp1 = new Speciality
			{
				Name = "c#"
			};
			Speciality sp2 = new Speciality
			{
				Name = "front"
			};
			List<Speciality> spec = new List<Speciality>();
			spec.Add(sp1);
			spec.Add(sp2);
			for (int i = 0; i < 20; i++)
			{
				Project temp = new Project
				{
					Name = names[i % 5],
					Img = url,
					Description = descr,
					Team = null,
					Specialities = spec
				};
				db.Projects.Add(temp);
				db.SaveChanges();
			}
			
		}

		private static void TestAdd4()
		{
			string[] tags = { "devblog", "razrab", "vlog", "news" };
			foreach (var t in tags) 
			{
				Tag temp = new Tag
				{
					Name = t
				};
				db.Tags.Add(temp);
				db.SaveChanges();
			}
			
		}

		private static void TestAdd5()
		{
			string[] names = { "devblog", "razrab", "vlog", "news" };
			string body =
				"Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
			string descr = body.Substring(0, 30);
			Project pr1 = new Project
			{
				Name = "1",
				Img = "123"
			};
			Project pr2 = new Project
			{
				Name = "2",
				Img = "d"
			};
			List<Project> kek = new List<Project>();
			kek.Add(pr1);
			kek.Add(pr2);
			for (int i = 0; i < 20; i++)
			{
				Team team = new Team
				{
					Name = names[i % 4],
					Description = descr,
					Members = TestAdd(),
					Projects = kek,
					Specialities = null
				};
				db.Teams.Add(team);
				db.SaveChanges();
			}
		}
		
		#endregion
		[Route("getArticles")]
		[HttpGet]
		public JsonResult GetArticles(int? skip, int? limit)
		{
			if (skip != null && limit != null)
			{
				Article[] arts = db.Articles
					.Include("Author")
					.Include("Tags")
					.Skip((int)skip)
					.Take((int)limit)
					.ToArray();
				return Json(arts);
			}
			throw new NullReferenceException();
			
			

		}
		

		[Route("getTopArticles")]
		[HttpGet]
		public JsonResult GetTopArticles()
		{
			Article[] arts = db.Articles
				.Include("Author")
				.Include("Tags")
				.OrderByDescending(d => d.Date)
				.Take(3)
				.ToArray();

			return Json(arts);
		}

		[Route("getArticle")] 
		[HttpGet]
		public JsonResult GetArticle(int? id)
		{
			if (id != null)
			{
				Article art = db.Articles
					.Include("Author")
					.Include("Tags")
					.FirstOrDefault(a => a.ArticleId == id) ;
				if (art != null)
				{
					return Json(art);
				}
				throw new NullReferenceException();
			}
			throw new NullReferenceException();
		}

		[Route("getTeams")]
		[HttpGet]
		public JsonResult GetTeams()
		{
			Team[] teams = db.Teams.Include("Members").ToArray();
			return Json(teams);
		}

		[Route("getTeamInfo")]
		[HttpGet]
		public JsonResult GetTeamInfo(int? id)
		{
			if (id != null)
			{
				Team team = db.Teams
					.Include("Projects")
					.Include("Members")
					.Include("Specialities")
					.FirstOrDefault(t => t.TeamId == id);
					
				if (team != null)
				{
					return Json(team);
				}
				throw new NullReferenceException();
			}
			throw new NullReferenceException();
		}

		[Route("getTeamMembers")]
		[HttpGet]
		public JsonResult GetTeamMembers(int? id)
		{
			if (id != null)
			{
				Team team = db.Teams
					.Include("Projects")
					.Include("Members")
					.Include("Specialities")
					.FirstOrDefault(t => t.TeamId == id);
				if (team != null)
				{
					List<User> mems = team.Members;
					return Json(mems);
				}
				throw new NullReferenceException();
			}
			throw new NullReferenceException();
			

			
		}

		[Route("getMembers")]
		[HttpGet]
		public JsonResult GetMembers(int? offset, int? count)
		{
			if (offset != null && count != null)
			{
				User[] users = db.Users
					.Include("Specialities")
					.Skip((int) offset)
					.Take((int) count)
					.ToArray();
				return Json(users);
			}
			throw new NullReferenceException();
			
		}
		
		[Route("getMemberInfo")]
		[HttpGet]
		public JsonResult GetMemberInfo(string id)
		{
			if (id != null)
			{
				User user = db.Users
					.Include("Specialities")
					.FirstOrDefault(u => u.Id == id);
					
				if (user != null)
				{
					return Json(user);
					
				}
				throw new NullReferenceException();
			}

			throw new NullReferenceException();
		}
		
		[Route("getProjects")]
		public JsonResult GetProjects()
		{
			Project[] projects = db.Projects
				.Include("Team")
				.Include("Members")
				.Include("Specialities")
				.ToArray();
			return Json(projects);
			
		}

		

		[Route("getTeam")]
		public JsonResult GetTeam(int? id)
		{
			if (id != null)
			{
				Team team = db.Teams
					.Include("Team")
					.Include("Members")
					.Include("Projects")
					.FirstOrDefault(t => t.Projects.Any(p => p.ProjectId == id));
				if (team != null)
				{
					return Json(team);
					
				}
				throw new NullReferenceException();
			}

			throw new NullReferenceException();
		}

		[Route("getTags")]
		[HttpGet]
		public JsonResult GetTags()
		{
			Tag[] tags = db.Tags.ToArray();
			return Json(tags);
		}

		[Route("getSpecialities")]
		[HttpGet]
		public JsonResult GetSpecialities()
		{
			Speciality[] specs = db.Specialities.ToArray();
			return Json(specs);
		}


		[Route("getProjectInfo")]
		[HttpGet]
		public JsonResult GetProjectInfo(int? id)
		{
			if (id != null)
			{
				Project pr = db.Projects
					.Include("Team")
					.Include("Members")
					.Include("Projects")
					.FirstOrDefault(p => p.ProjectId == id);
				if (pr != null)
				{
					return Json(pr);
				}
				throw new NullReferenceException();
			}
			throw new NullReferenceException();
			
		}
		
	}
}
