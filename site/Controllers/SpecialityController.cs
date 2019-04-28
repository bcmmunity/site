using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using site.Models;
using site.ViewModels;

namespace site.Controllers
{
    public class SpecialityController : Controller
    {

        private readonly ApplicationContext _db;
        private readonly UserManager<User> _userManager;
        public SpecialityController(ApplicationContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        
        public IActionResult Add()
        {
            // TODO: РОЛИ РОЛИ РОЛИ
            string nikita = _userManager.GetUserId(HttpContext.User);
            ViewBag.nikita = nikita;
            if (nikita != "87759cdf-3b58-483b-a738-f79a051bac23")
            {
                return NotFound();
            }
            ViewBag.sp = _db.Specialities.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(SpecialityViewModel model)
        {
            // TODO: РОЛИ РОЛИ РОЛИ
            string nikita = _userManager.GetUserId(HttpContext.User);
            ViewBag.nikita = nikita;
            if (nikita != "87759cdf-3b58-483b-a738-f79a051bac23")
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                foreach (var name in model.Names)
                {
                    Speciality spec = new Speciality
                    {
                        Name = name
                    };
                    await _db.Specialities.AddAsync(spec);
                    await _db.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index", "Home");

        }
    }
}