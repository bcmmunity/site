using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using site.Models;
using site.ViewModels;

namespace site.Controllers
{
    public class SpecialityController : Controller
    {
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(SpecialityViewModel model)
        {
            foreach (var specialityViewModel in model.Names)
            {
                Console.WriteLine(specialityViewModel);
            }
            if (ModelState.IsValid)
            {
                foreach (var name in model.Names)
                {
                    Speciality spec = new Speciality
                    {
                        Name = name
                    };
                    MainController.db.Specialities.Add(spec);
                    MainController.db.SaveChanges();
                }
                

                
            }
            return View();
            
        }
    }
}