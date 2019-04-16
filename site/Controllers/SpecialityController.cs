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

        private ApplicationContext _db;

        public SpecialityController(ApplicationContext db)
        {
            _db = db;
        }
        
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(SpecialityViewModel model)
        {
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
            return View();
            
        }
    }
}