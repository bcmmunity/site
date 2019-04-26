using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using site.Models;
using site.ViewModels;

namespace site.Controllers
{
    public class ExperienceController : Controller
    {
        private UserManager<User> _userManager;
        private ApplicationContext _db;

        public ExperienceController(UserManager<User> userManager, ApplicationContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        
        public async Task<IActionResult> Edit(int id)
        {
            User user = await _db.Users.Include(e => e.Experiences).FirstOrDefaultAsync(u => u.Id == _userManager.GetUserId(User));
            Experience exp = user.Experiences.FirstOrDefault(l => l.ExperienceId == id);
            if (exp != null)
            {
                ExperienceViewModel viewModel = new ExperienceViewModel
                {
                    Id = exp.ExperienceId,
                    Title = exp.Title,
                    Link = exp.Link,
                    Description = exp.Description,
                    StartDate = exp.StartDate,
                    FinishDate = exp.FinishDate,
                    IsWork = exp.IsWork,
                    IsNow = exp.Now
                };
                return View(viewModel);
            }
            else
            {
                return NotFound();
            }
            
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(ExperienceViewModel model)
        {
            if (ModelState.IsValid)
            {    
                User user = await _db.Users.Include(e => e.Experiences).FirstOrDefaultAsync(u => u.Id == _userManager.GetUserId(User));
                Experience exp = user.Experiences.FirstOrDefault(l => l.ExperienceId == model.Id);
                if (exp != null)
                {
                    exp.Title = model.Title;
                    exp.Link = model.Link;
                    exp.Description = model.Description;
                    exp.IsWork = model.IsWork;
                    exp.Now = model.IsNow;
                    exp.StartDate = model.StartDate;
                    exp.FinishDate = model.FinishDate;
                }

                _db.Users.Update(user);
                await _db.SaveChangesAsync();

            }
                
            

            return RedirectToAction("Index", "Home");
            
        }

        public async Task<IActionResult> Delete(int id)
        {
            User user = await _db.Users.Include(e => e.Experiences).FirstOrDefaultAsync(u => u.Id == _userManager.GetUserId(User));
            Experience exp = user.Experiences.FirstOrDefault(l => l.ExperienceId == id);
            user.Experiences.Remove(exp);
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
                
            return RedirectToAction("Profile", "Account");
        }
    }
}