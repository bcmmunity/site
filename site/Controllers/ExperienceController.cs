using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using site.ViewModels;

namespace site.Controllers
{
    public class ExperienceController : Controller
    {
        public IActionResult Add(ExperienceViewModel model)
        {
            return View(model);
        }
        // GET
//        [Authorize]
        [HttpPost]
        public IActionResult Keki(ExperienceViewModel model)
        {
            for (var i = 0; i < model.Title.Length; i++)
            {
                Console.WriteLine("\n\n\n\n");
                Console.WriteLine(model.Title[i]);
                Console.WriteLine(model.Link[i]);
                Console.WriteLine(model.Description[i]);
                Console.WriteLine("\n\n\n\n"); 
            }
            

            return RedirectToAction("Add", model);
            
        }
    }
}