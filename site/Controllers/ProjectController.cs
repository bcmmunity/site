using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using site.Models;
using site.ViewModels;

namespace site.Controllers
{
    public class ProjectController : Controller
    {
        // GET
        private string _bd = "Server=localhost\\SQLEXPRESS;Database=basa42;Trusted_Connection=True;";
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("\n\n\n\n");
                Console.WriteLine(model.Cover);
                Console.WriteLine(model.Description);
                Console.WriteLine(model.Title);
                Console.WriteLine(model.SliderImages);
                foreach (var modelSpec in model.Specs)
                {
                    Console.WriteLine(modelSpec);
                }
                foreach (var modelSpec in model.Members)
                {
                    Console.WriteLine(modelSpec);
                }
                Console.WriteLine("\n\n\n\n");

//            Project proj = new Project
//            {
//                Name = model.Title,
//                Img = model.Cover,
//                Description = model.Description,
//                Rang = MainController.db.Projects.ToList().Count + 1,
//
//                SliderImages = model.SliderImages.Split(" ").ToList(),
//            };
//            List<Speciality> sps = new  List<Speciality>();
//            foreach (var id in model.Specs)
//            {
//                sps.Add(MainController.db.Specialities.Find(id));
//            }
//            Team team = new Team();
//            foreach (var id in model.Members)
//            {
//                team.Members.Add(MainController.db.Users.Find(id));
//            }
//            proj.Specialities.AddRange(sps);
//            proj.Team = team;
//            MainController.db.Projects.Add(proj);
//            MainController.db.SaveChanges();
//            MainController.db.Teams.Add(team);
//            MainController.db.SaveChanges();
                
            }
            return View(model);
            
        }
    }
}