﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using site.Models;
using site.ViewModels;

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
			return PartialView("Members", _db.Users.OrderBy(d => d.Rang).Include(u => u.Links).Skip(offset).Take(count).ToArray());
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
		
		[HttpPost]
		public ActionResult SendMail(SendMailViewModel mail)
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
			m.Body = $"От: {mailFrom}\n{body}";
			SmtpClient smtp = new SmtpClient("wpl19.hosting.reg.ru", 587 );
			smtp.Credentials = new NetworkCredential("info@diffind.com", "Diffind123!");
			smtp.Send(m);
			return RedirectToAction("Index", "Home");
		}
	}
}
