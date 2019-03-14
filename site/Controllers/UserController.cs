using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using site.Models;
using site.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CustomIdentityApp.Controllers
{
	public class UserController : Controller
	{
		UserManager<User> _userManager;

		public UserController(UserManager<User> userManager)
		{
			_userManager = userManager;
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

			EditUserViewModel model = new EditUserViewModel { Id = user.Id, Email = user.Email, Description = user.Description, Photo = user.Photo, Position = user.Position, Name = user.Name, Surname = user.Surname };
			return View(model);
		}

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Edit(EditUserViewModel model)
		{
			if (ModelState.IsValid)
			{
				User user = await _userManager.FindByIdAsync(model.Id);
				if (user != null)
				{
					user.Email = model.Email;
					user.UserName = model.Email;
					user.Description = model.Description;
					user.Photo = model.Photo;
					user.Position = model.Position;
					user.Name = model.Name;
					user.Surname = model.Surname;
					

					var result = await _userManager.UpdateAsync(user);
					if (result.Succeeded)
					{
						//return Redirect("/Home/About");
						return RedirectToAction("About", "Home");
						//return RedirectToActi	on("Index");
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