using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using site.Models;
using site.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace CustomIdentityApp.Controllers
{
	public class UserController : Controller
	{
		private UserManager<User> _userManager;
		private ApplicationContext _db;
		IHostingEnvironment _contentPath;
		public UserController(UserManager<User> userManager, ApplicationContext db, IHostingEnvironment contentPath)
		{
			_userManager = userManager;
			_db = db;
			_contentPath = contentPath;
		}
		
		
		
		public IActionResult Index() => View(_userManager.Users.ToList());

		public IActionResult Create() => View();

		[HttpPost]
		public async Task<IActionResult> Create(CreateUserViewModel model)
		{
			if (ModelState.IsValid)
			{
				User user = new User { Email = model.Email, UserName = model.Email };
				var result = await _userManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					return RedirectToAction("Index");
				}
				else
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
				}
			}
			return View(model);
		}

		[Authorize]
		public async Task<IActionResult> Edit()
		{
			User user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
			if (user == null)
			{
				return NotFound();
			}
			ViewBag.socials = _db.SNs.ToList();
			List<string> links = _db.Users
				.Include(l => l.Links)
				.FirstOrDefault(u => u.Id == _userManager.GetUserId(HttpContext.User))
				?.Links
				.Select(l => l.Href)
				.ToList();
			EditUserViewModel model = new EditUserViewModel { Id = user.Id,Links = links, Email = user.Email, Description = user.Description, Position = user.Position, Name = user.Name, Surname = user.Surname};
			return View(model);
		}

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Edit(EditUserViewModel model)
		{
			
			if (ModelState.IsValid)
			{
				ViewBag.socials = _db.SNs.ToList();
				List<SN> socials = _db.SNs.ToList();

				// Выглядит как мега костыль но он работает
				// TODO: Исправить костыль, если он является костылем	

				#region Удаление предыдущих ссылок

				User tempUser = _db.Users.Include(u => u.Links).FirstOrDefault(u => u.Id == model.Id);
				if (tempUser != null && tempUser.Links.Count != 0)
				{
					tempUser.Links.Clear();
					_db.Users.Update(tempUser);
					_db.SaveChanges();
				}

				
							
				#endregion
				
				User user = await _userManager.FindByIdAsync(model.Id);
				
				if (user != null)
				{
					string path;
					
					user.Email = model.Email;
					user.UserName = model.Email;
					user.Description = model.Description;
					user.Position = model.Position;
					user.Name = model.Name;
					user.Surname = model.Surname;
					
					// Сохранение фотографии профиля в папку пользователя
					if (model.Photo != null)
					{
						if (model.Photo.ContentType.StartsWith("image"))
						{
							// Проверка, создание нужных папок, переключение текущей директории
							Directory.SetCurrentDirectory(_contentPath.WebRootPath + "/img/");
							if (!Directory.Exists("UserPhotos"))
								Directory.CreateDirectory("UserPhotos");
							Directory.SetCurrentDirectory(_contentPath.WebRootPath + "/img/UserPhotos");
							if (!Directory.Exists(model.Id))
								Directory.CreateDirectory(model.Id);
							Directory.SetCurrentDirectory(model.Id);
							
							path = model.Photo.FileName;
							
							Image img = Image.FromStream(model.Photo.OpenReadStream());
							
							// Кроп изображения
							// http://web.archive.org/web/20130126075626/http://www.switchonthecode.com:80/tutorials/csharp-tutorial-image-editing-saving-cropping-and-resizing
	
							using (Bitmap bitmap = new Bitmap(img))
							{
								Rectangle cropArea = new Rectangle(
									
									(int) (img.Size.Width * ((double) model.CropX / 600)),
									(int) (img.Size.Height * ((double)model.CropY / 600)),
									(int) (img.Size.Width * ((double) model.CropWidth / 600)),
									(int) (img.Size.Width * ((double) model.CropWidth / 600)));
								bitmap.Clone(cropArea, bitmap.PixelFormat).Save(model.Photo.FileName);
							}
							
							
				
							user.Photo = $"/img/UserPhotos/{model.Id}/{path}";
						}
					}
					
					// Возвращаем изначальную директорию
					Directory.SetCurrentDirectory(_contentPath.ContentRootPath);
					
					
					// Добавление опыта работы, курсов и т.д.
					if (model.Links != null) 
						for (var i = 0; i < model.Links.Count; i++)
						{
							Experience exp = new Experience();
							if (model.Links[i] != null)
							{
								exp.Link = model.Links[i];
							}
							if (model.Descriptions[i] != null)
							{
								exp.Description = model.Descriptions[i];
							}
							if (model.Titles[i] != null)
							{
								exp.Title = model.Titles[i];
							}
	
							exp.StartDate = model.StartDates[i];
							exp.FinishDate = model.FinishDates[i];
							if (model.Nows != null)
								exp.Now = model.Nows[i];
							else
								exp.Now = false;
							if (model.IsWorks != null)
								exp.IsWork = model.IsWorks[i];
							else
								exp.IsWork = false;
							user.Experiences.Add(exp);
						}

					// Добавление ссылок на соц. сети
					if (model.Socials != null)
					{
						for (var i = 0; i < model.Socials.Count; i++)
						{

							if (model.Socials[i] == null || model.Socials[i] == "") 
								user.Links.Add(new Link
								{
									IsEmpty = true
								});
							else
							{
								Link soc = new Link
								{
									IsSocial = true,
									Href = model.Socials[i],
									Pic = socials[i].Pic,
									Title = socials[i].Title 
								};
								user.Links.Add(soc);
							}
						
								
						}
					}
					
			
					var result = await _userManager.UpdateAsync(user);
					
					if (result.Succeeded)
					{
						return RedirectToAction("Profile", "Account");
					}
					else
					{
						foreach (var error in result.Errors)
						{
							ModelState.AddModelError(string.Empty, error.Description);
						}
					}
				}
			}
			return View(model);
		}

		[HttpPost]
		public async Task<ActionResult> Delete(string id)
		{
			User user = await _userManager.FindByIdAsync(id);
			if (user != null)
			{
				IdentityResult result = await _userManager.DeleteAsync(user);
			}
			return RedirectToAction("Index");
		}
	}
}