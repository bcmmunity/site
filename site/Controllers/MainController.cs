using System;
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
		
		public MainController(ApplicationContext _db)
		{
			db = _db;
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
		
		private static void AddSN()
		{
			string[] snNames =
			{
				"twitter",
				"facebook",
				"vk",
				"instagram",
				"whatsapp",
				"behance",
				"github",
				"youtube"
			};
			for (int i = 0; i < snNames.Length; i++)
			{
				SN tag = new SN
				{
					Title = snNames[i],
					Pic = $"../../img/{snNames[i].ToLower()}.svg"
				};
				db.SNs.Add(tag);
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
			for (int i = 0; i < 8; i++)
			{
				Random rand = new Random();
				List<Speciality> sp = db.Specialities
					.Take(rand.Next(1, db.Specialities.ToArray().Length))
					.ToList();
				Link soc = new Link
				{
					Href = "https://youtube.com",
				};
				Experience exp = new Experience
				{
					Title = "Название",
					Description = "Какое то крутое описание того где я когда то работал, помогите",
					Link = "https://vk.com",
					StartDate = new DateTime(2016, 10, 10),
					FinishDate = new DateTime(rand.Next(2016, 2018), 10, 10)
				};
				User user = new User
				{
					Rang = i,
					Photo = photo,
					Specialities = sp,
					Name = names[rand.Next(0, names.Length)],
					Surname = surnames[rand.Next(0, surnames.Length)],
					Position = positions[rand.Next(0, positions.Length)],
					Description = descr,
					Email = mails[i % 8],
					Links = {soc, soc, soc},
					Experiences = {exp, exp, exp},
					
				};

				
				db.Users.Add(user);
				db.SaveChanges();
				Thread.Sleep(20);
			}

		}

		public static void AddProjects()
		{
			string photo = "https://loremflickr.com/1280/720";
			string[] names =
			{
				"Сайт", "GoW", "Пожарка", "Чат-бот с расписанием", "Электронный журнал", "Крутой проект 1", "Крутой проект 2", "Проект 3", "Проект 4"
			};
			string[] images = {"https://loremflickr.com/1280/720", "https://loremflickr.com/1280/720", "https://loremflickr.com/1280/720"};
			string descr = "Lorem Ipsum - это текст-\"рыба\", часто используемый в печати и вэб-дизайне. Lorem Ipsum является стандартной \"рыбой\" для текстов на латинице с начала XVI века. В то время некий безымянный печатник создал большую коллекцию размеров и форм шрифтов, используя Lorem Ipsum для распечатки образцов. ";
			for (int i = 0; i < 10; i++)
			{
				Random rand = new Random();
				List<Speciality> sp = db.Specialities
					.Take(rand.Next(1, db.Specialities.ToArray().Length))
					.ToList();
				Link link = new Link
				{
					Href = "https://youtube.com",
//					Type = db.SNs.ToList().GetRandomItem()
				};
				Project project = new Project
				{
					Img = photo,
					Name = names[i % names.Length],
					Description = descr,
					Specialities = sp,
					SliderImages = images.ToList(),
					Links = {link, link},
                    							
				};

				
				db.Projects.Add(project);
				db.SaveChanges();
				project.Specialities = sp;
				db.Update(project);
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
				List<User> mems = db.Users
					.Take(rand.Next(1, db.Users.ToArray().Length - 2))
					.ToList();
				Team team = new Team
				{
					Name = names[i],
					Description = descr
				};
				team.Members.AddRange(mems);

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
			for (int i = 0; i < 30; i++)
			{
				Random rand = new Random();
				List<Tag> tags = db.Tags
					.Take(rand.Next(1, db.Tags.ToArray().Length))
					.ToList();
				Article art = new Article
				{
					PhotoCover = "https://loremflickr.com/1920/1080",
					Name = names[rand.Next(0, 6)],
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

		public static void AddTeamToProject()
		{
			List<Project> pr = db.Projects.ToList();
			foreach (var p in pr)
			{
				p.Team = db.Teams.ToList().GetRandomItem();
//				p.Specialities = db.Specialities.ToList();
				db.Update(p);
				db.SaveChanges();
			}
			
			
		}


		public static void AddProjectsToUser()
		{
			List<User> users = db.Users.ToList();
			foreach (var u in users)
			{
				u.Projects.Add(db.Projects.ToList().GetRandomItem());
				u.Projects.Add(db.Projects.ToList().GetRandomItem());
				u.Projects.Add(db.Projects.ToList().GetRandomItem());
				db.Update(u);
				db.SaveChanges();
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
//
//		[Route("getTeams")]
//		[HttpGet]
//		public JsonResult GetTeams()
//		{
//			Team[] teams = db.Teams.ToArray();
//			return Json(teams);
//		}
//
//		[Route("getTeamInfo")]
//		[HttpGet]
//		public JsonResult GetTeamInfo(int? id)
//		{
//			if (id != null)
//			{
//				Team team = db.Teams
//					.FirstOrDefault(t => t.TeamId == id);
//					
//				if (team != null)
//				{
//					return Json(team);
//				}
//				throw new NullReferenceException();
//			}
//			throw new NullReferenceException();
//		}
//
//		[Route("getTeamMembers")]
//		[HttpGet]
//		public JsonResult GetTeamMembers(int? id)
//		{
//			if (id != null)
//			{
//				Team team = db.Teams
//					.FirstOrDefault(t => t.TeamId == id);
//				if (team != null)
//				{
//					List<User> mems = team.Members;
//					return Json(mems);
//				}
//				throw new NullReferenceException();
//			}
//			throw new NullReferenceException();
//			
//
//			
//		}
//
//		[Route("getMembers")]
//		[HttpGet]
//		public JsonResult GetMembers(int? offset, int? count)
//		{
//			if (offset != null && count != null)
//			{
//				User[] users = db.Users
////					.Include("Specialities")
//					.Skip((int) offset)
//					.Take((int) count)
//					.ToArray();
//				return Json(users);
//			}
//			throw new NullReferenceException();
//			
//		}
//		
//		[Route("getMemberInfo")]
//		[HttpGet]
//		public JsonResult GetMemberInfo(string id)
//		{
//			if (id != null)
//			{
//				User user = db.Users
//					.FirstOrDefault(u => u.Id == id);
//					
//				if (user != null)
//				{
//					return Json(user);
//					
//				}
//				throw new NullReferenceException();
//			}
//
//			throw new NullReferenceException();
//		}
//		
//		[Route("getProjects")]
//		[HttpGet]
//		public JsonResult GetProjects(int? offset, int? count)
//		{
//			if (offset != null && count != null)
//			{
//				Project[] projects = db.Projects
//					.Skip((int)offset)
//					.Take((int)count)
//					.ToArray();
//				return Json(projects);
//			}
//			throw new NullReferenceException();
//			
//			
//		}
//
//		
//
//		[Route("getTeam")]
//		public JsonResult GetTeam(int? id)
//		{
//			if (id != null)
//			{
//				Team team = db.Teams
//					.FirstOrDefault(t => t.Projects.Any(p => p.ProjectId == id));
//				if (team != null)
//				{
//					return Json(team);
//					
//				}
//				throw new NullReferenceException();
//			}
//
//			throw new NullReferenceException();
//		}
//
//		[Route("getTags")]
//		[HttpGet]
//		public JsonResult GetTags()
//		{
//			Tag[] tags = db.Tags.ToArray();
//			return Json(tags);
//		}
//
//		[Route("getSpecialities")]
//		[HttpGet]
//		public JsonResult GetSpecialities()
//		{
//			Speciality[] specs = db.Specialities.ToArray();
//			return Json(specs);
//		}
//
//
//		[Route("getProjectInfo")]
//		[HttpGet]
//		public JsonResult GetProjectInfo(int? id)
//		{
//			if (id != null)
//			{
//				Project pr = db.Projects
//					.FirstOrDefault(p => p.ProjectId == id);
//				if (pr != null)
//				{
//					return Json(pr);
//				}
//				throw new NullReferenceException();
//			}
//			throw new NullReferenceException();
//			
//		}

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
