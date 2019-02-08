﻿using System;
using Microsoft.AspNetCore.Mvc;
using site.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.CodeAnalysis;
using Project = site.Models.Project;

namespace site.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MainController : Controller
	{
		public static ApplicationContext db;
		
		
		public static void Unit()
		{
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
			optionsBuilder.UseSqlServer("Server=localhost;Database=u0641156_diffind;User Id = u0641156_diffind; Password = Qwartet123!");
//			optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=q112;Trusted_Connection=True;");
//			optionsBuilder.UseSqlServer(
//				"Server=localhost\\SQLEXPRESS;Database=t49;Trusted_Connection=True;");
			db = new ApplicationContext(optionsBuilder.Options);

//				AddTags();
//				AddSpecialities();
//				AddUsers();
//				AddProjects();
//				AddTeams();
//				AddArticles();
		}
		
		#region Test data
		private static void AddTags()
		{
			string[] tagNames =
			{
				"AI", 
				"ML",
				"Unity", 
				"announcement", 
				"News", 
				"DevBlog"
			};
			foreach (var name in tagNames)
			{
				Tag tag = new Tag
				{
					Name = name
				};
				db.Tags.Add(tag);
				db.SaveChanges();
			}
		}

		private static void AddSpecialities()
		{
			string[] specs =
			{
				"C#", 
				"Python", 
				"UX/UI", 
				"React", 
				"HTML/CSS"
			};

			foreach (var sp in specs)
			{
				Speciality spec = new Speciality
				{
					Name = sp
				};
				db.Specialities.Add(spec);
				db.SaveChanges();
			}
		}

		public static void AddUsers()
		{
			string photo = "https://loremflickr.com/320/240";
			string[] names =
			{
				"Лариса", "Любим", "Акулина", "Флорентин", "Измаил", "Серафим", "Федот", "Онуфрий", "Севастьян", "Владимир"
			};
			string[] surnames =
			{
				"Тарасов", "Крылова", "Потапова", "Попова", "Соколов", "Андреева", "Романова", "Кабанов", "Стрелкова", "Лукин"
			};
			string[] positions =
			{
				"Тимлид", "Сеньор", "Джун", "Миддл", "Редактор", "Гендир"
			};
			string[] mails =
			{
				"mcarter@nelson.net", "colemantaylor@joyce.org", "hbrown@gmail.com", "lisabowman@yahoo.com", "francisaustin@yahoo.com", "johnmanning@gonzalez.com", "paul89@lopez.info", "fgutierrez@gmail.com", "sheila06@chandler-gonzalez.com", "downsrenee@lee.org", "lambertcandice@ray-campos.com", "goodroger@hotmail.com", "lkelly@lewis-carr.biz", "brianrich@george.com", "cooleysue@smith.com", "gsherman@yahoo.com", "dchoi@cunningham.biz", "michaelgonzalez@walters-bryant.com", "marissamccormick@peters.info", "kimberlyrose@petersen-chapman.com", "stephanie96@gmail.com", "nelsonchristopher@savage.com", "rossjamie@hotmail.com", "oconnellevelyn@hotmail.com", "rnelson@stone.info", "melindaramos@kent.com", "scole@hotmail.com", "dlin@gmail.com", "timothybarrera@clarke.com", "hruiz@thomas-estes.com", "bradley63@gmail.com", "broberts@thompson.com", "christy54@vargas.info", "rfoster@yahoo.com", "edward23@yahoo.com", "qsutton@lewis.net", "nicholsshelia@hotmail.com", "tiffanycaldwell@houston-kaiser.org", "nelsoncory@lynch.com", "ericwilliams@king.biz"
			};
			string descr = "Lorem Ipsum - это текст-\"рыба\", часто используемый в печати и вэб-дизайне. Lorem Ipsum является стандартной \"рыбой\" для текстов на латинице с начала XVI века. В то время некий безымянный печатник создал большую коллекцию размеров и форм шрифтов, используя Lorem Ipsum для распечатки образцов. ";
			for (int i = 0; i < 6; i++)
			{
				Random rand = new Random();
				List<Speciality> sp = db.Specialities
					.Take(rand.Next(1, db.Specialities.ToArray().Length))
					.ToList();
				Models.User user = new User
				{
					Photo = photo,
					Name = names[rand.Next(0, names.Length)],
					Surname = surnames[rand.Next(0, surnames.Length)],
					Position = positions[rand.Next(0, positions.Length)],
					Description = descr,
					Email = mails[i]
					
				};

				foreach (var s in sp)
				{
					user.Specialities.Add(s);
				}
				
				db.Users.Add(user);
				db.SaveChanges();
				Thread.Sleep(20);
			}

		}

		public static void AddProjects()
		{
			string photo = "https://loremflickr.com/cache/resized/7890_46965722211_26f02453fd_h_1000_1000_nofilter.jpg";
			string[] names =
			{
				"Сайт", "GoW", "Пожарка", "Чат-бот с расписанием", "Электронный журнал", "Крутой проект 1", "Крутой проект 2", "Проект 3", "Проект 4"
			};
			string descr = "Lorem Ipsum - это текст-\"рыба\", часто используемый в печати и вэб-дизайне. Lorem Ipsum является стандартной \"рыбой\" для текстов на латинице с начала XVI века. В то время некий безымянный печатник создал большую коллекцию размеров и форм шрифтов, используя Lorem Ipsum для распечатки образцов. ";
			for (int i = 0; i < names.Length * 2; i++)
			{
				Random rand = new Random();
				List<Speciality> sp = db.Specialities
					.Take(rand.Next(1, db.Specialities.ToArray().Length))
					.ToList();
				Project project = new Project
				{
					Img = photo,
					Name = names[i % names.Length],
					Description = descr,
					
				};
				project.Specialities.AddRange(sp);
				
				
				db.Projects.Add(project);
				db.SaveChanges();
				Thread.Sleep(10);
			}
		}

		public static void AddTeams()
		{
			string[] names =
			{
				"Команда разработки сайта", "Команда дизайнеров", "Команда фронт-энда", "Призраки", "Лень еще придумывать названик"
			};
			string descr =
				"Lorem Ipsum - это текст-\"рыба\", часто используемый в печати и вэб-дизайне. Lorem Ipsum является стандартной \"рыбой\" для текстов";
			for (int i = 0; i < 5; i++)
			{
				Random rand = new Random();
				List<Speciality> sp = db.Specialities
					.Take(rand.Next(1, db.Specialities.ToArray().Length - 1))
					.ToList();
				List<User> mems = db.Users
					.Take(rand.Next(1, db.Users.ToArray().Length - 2))
					.ToList();
				List<Project> prs = db.Projects
					.Take(rand.Next(1, db.Projects.ToArray().Length - 2))
					.ToList();
				Team team = new Team
				{
					Name = names[i],
					Description = descr
				};
				team.Specialities.AddRange(sp);
				team.Members.AddRange(mems);
				team.Projects.AddRange(prs);

				db.Teams.Add(team);
				db.SaveChanges();
				Thread.Sleep(15);
			}
		}

		public static void AddArticles()
		{
			string[] names =
			{
				"Как написать сайт и не сойти с ума", "Пишем тестовые данные для сайта, чтобы правдоподобно",
				"Чат-бот для маленьких и тупых (как автор)", "Столько всяких способов сделать одно и то же",
				"Тайм-менеджемент для бедных и для тех у кого нет времени",
				"Как бесполезно тратить деньги"
				
			};
			string body = "Lorem Ipsum - это текст-\"рыба\", часто используемый в печати и вэб-дизайне. Lorem Ipsum является стандартной \"рыбой\" для текстов на латинице с начала XVI века. В то время некий безымянный печатник создал большую коллекцию размеров и форм шрифтов, используя Lorem Ipsum для распечатки образцов. ";
			string descr = body.Substring(0, 30);
			for (int i = 0; i < 6; i++)
			{
				Random rand = new Random();
				List<Tag> tags = db.Tags
					.Take(rand.Next(1, db.Tags.ToArray().Length))
					.ToList();
				Article art = new Article
				{
					Name = names[i],
					Body = body,
					Description = descr, 
					Author = db.Users.ToList().GetRandomItem(),
					Date = new DateTime(2019, 1, i + 1)
				};
				art.Tags.AddRange(tags);
				db.Articles.Add(art);
				db.SaveChanges();
				Thread.Sleep(15);
			}
		}
		
		#endregion		

		[Route("getArticles")]
		[HttpGet]
		public JsonResult GetArticles(int? offset, int? count)
		{
			if (offset != null && count != null)
			{
				Article[] arts = db.Articles
					.Include("Author")
					.Skip((int)offset)
					.Take((int)count)
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
			Team[] teams = db.Teams.ToArray();
			return Json(teams);
		}

		[Route("getTeamInfo")]
		[HttpGet]
		public JsonResult GetTeamInfo(int? id)
		{
			if (id != null)
			{
				Team team = db.Teams
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
//					.Include("Specialities")
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
		[HttpGet]
		public JsonResult GetProjects(int? offset, int? count)
		{
			if (offset != null && count != null)
			{
				Project[] projects = db.Projects
					.Skip((int)offset)
					.Take((int)count)
					.ToArray();
				return Json(projects);
			}
			throw new NullReferenceException();
			
			
		}

		

		[Route("getTeam")]
		public JsonResult GetTeam(int? id)
		{
			if (id != null)
			{
				Team team = db.Teams
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
					.FirstOrDefault(p => p.ProjectId == id);
				if (pr != null)
				{
					return Json(pr);
				}
				throw new NullReferenceException();
			}
			throw new NullReferenceException();
			
		}

		[Route("sendMail")]
		[HttpPost]
		public JsonResult SendMail([FromBody] Mail mail)
		{
			if (mail == null)
			{
				throw new NullReferenceException();
			}

			string mailFrom = mail.From;
			string mailTo = "info@diffind.com";
			string displayName = mail.Name;
			string header = mail.Header;
			string body = mail.Body;
			MailAddress from = new MailAddress(mailFrom, displayName);
			MailAddress to = new MailAddress(mailTo);
			MailMessage m = new MailMessage();
			m.From = from;
			m.To.Add(to);
			m.Subject = header;
			m.Body = body;
			SmtpClient smtp = new SmtpClient("wpl19.hosting.reg.ru", 587 );
			smtp.Credentials = new NetworkCredential("info@diffind.com", "SuperInfo123!");
			smtp.Send(m);
			return Json(new
			{
				response = "Ваша заявка отправлена"
			});
		}

		
		
	}
}
