﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using site.ViewModels;
using site.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace site.Controllers
{
	//[Route("api/[controller]")]
	//[ApiController]
	public class AccountController : Controller
	{
		private readonly UserManager<User> _userManager;//= new UserManager<User, string>(new UserStore<ApplicationUser, CustomRole, string, CustomUserLogin, CustomUserRole, CustomUserClaim>(new myDbContext()));
		private readonly SignInManager<User> _signInManager;
		private readonly ApplicationContext _db;

		public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationContext db)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_db = db;
		}
		
		[HttpGet]
		public IActionResult Register()
		{
			return View();
			
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				int nextRang = _db.Users.ToList().Count + 1;
				User user = new User { Email = model.Email, UserName = model.Email, Rang = nextRang};
				// добавляем пользователя
				var result = await _userManager.CreateAsync(user, model.Password);
				ViewBag.UserId = user.Id;
				if (result.Succeeded)
				{
					// установка куки
					await _signInManager.SignInAsync(user, false);
					return RedirectToAction("Index", "Home");
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

		[HttpGet]
		public IActionResult Login(string returnUrl = null)
		{
			return View();//new LoginViewModel { ReturnUrl = returnUrl });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				
				var result =
					await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
				
				if (result.Succeeded)
				{
					return RedirectToAction("Index", "Home");
				}
				else
				{
					ModelState.AddModelError("", "Неправильный логин и (или) пароль");
				}
			}
			return View(model);
		}

		//[HttpPost]
		//[ValidateAntiForgeryToken]
		public async Task<IActionResult> LogOff()
		{
			// удаляем аутентификационные куки
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		
		}
		
		public  ActionResult Profile(string id)
		{
			string localId = id;
			ViewBag.CurrentUserId = _userManager.GetUserId(HttpContext.User);;
			if (id == null)
			{
				localId = _userManager.GetUserId(HttpContext.User);
			}
			User user = _db.Users
				.Include(s => s.Links)
				.Include(sp => sp.Specialities)
				.ThenInclude(u => u.Speciality)
				.Include(s => s.Experiences)
				.Include(p => p.Projects)
				.ThenInclude(p => p.Project)
				
				.FirstOrDefault(u => u.Id == localId);
			
			if (user != null)
			{
				return View("Profile", user);
			}
			else
			{
				return View("Error");
			}
		}


		//[Route("register")]
		//	[HttpPost]
		//	public async Task<IActionResult> Register(RegisterViewModel model)
		//	{
		//		if (ModelState.IsValid)
		//		{
		//			User user = new User { Email = model.Email, UserName = model.Email };
		//			// добавляем пользователя

		//			var result = await _userManager.CreateAsync(user, model.Password);

		//			if (result.Succeeded)
		//			{
		//				// установка куки
		//				await _signInManager.SignInAsync(user, false);
		//				return Json("Ok");
		//			}
		//			else
		//			{
		//				foreach (var error in result.Errors)
		//				{
		//					ModelState.AddModelError(string.Empty, error.Description);
		//				}
		//			}
		//		}
		//		return Json("Exception");
		//	}

		//	//[Route("logIn")]
		//	[HttpPost]
		//	//[ValidateAntiForgeryToken]
		//	public async Task<IActionResult> Login(LoginViewModel model)
		//	{
		//		if (ModelState.IsValid)
		//		{
		//			var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
		//			if (result.Succeeded)
		//			{
		//				return Json("Вы вошли");
		//			}
		//			else
		//			{
		//				return Json("Неправильный логин и (или) пароль");
		//			}
		//		}
		//		return Json("Вы вошли");
		//	}

		//	//[Route("logOff")]
		//	[HttpPost]
		//	//[ValidateAntiForgeryToken]
		//	public async Task<IActionResult> LogOff()
		//	{
		//		// удаляем аутентификационные куки
		//		await _signInManager.SignOutAsync();
		//		return Json("Вы вышли");
		//	}
	}
}