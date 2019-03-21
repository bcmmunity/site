using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Remotion.Linq.Clauses;
using site.Models;
using site.ViewModels;

namespace site.Controllers
{
    public class ProjectController : Controller
    {

		public ProjectController(ApplicationContext db)
		{

		}

		// GET
		private string _bd = "Server=localhost\\SQLEXPRESS;Database=f1;Trusted_Connection=True;";
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine(model.Cover.ContentType);
                    if (model.Cover != null)
                    {
                        if (model.Cover.ContentType.StartsWith("image"))
                        {
                            SavePhoto(model.Cover);    
                        }
                        else
                        {
                            throw new ArgumentException();
                        }
                        
                    }
                    else
                    {
                        throw new ArgumentNullException();
                    }
                if (model.SliderImages != null)
                {
                    foreach (var image in model.SliderImages)
                    {
                        if (!image.ContentType.StartsWith("image"))
                        {
                            throw new ArgumentException();
                        }
                    }
                    SavePhoto(model.SliderImages);
                }
                else
                {
                    throw new ArgumentNullException();
                }

//                Project proj = new Project
//                {
//                    Name = model.Title,
//                    Img = model.Cover.Name,
//                    Description = model.Description,
//                    Rang = MainController.db.Projects.ToList().Count + 1,
//
//                    SliderImages = model.SliderImages.Split(" ").ToList(),
//                };
//                List<Speciality> sps = new  List<Speciality>();
//                foreach (var id in model.Specs)
//                {
//                    sps.Add(MainController.db.Specialities.Find(id));
//                }
//                Team team = new Team();
//                foreach (var id in model.Members)
//                {
//                    team.Members.Add(MainController.db.Users.Find(id));
//                }
//                proj.Specialities.AddRange(sps);
//                proj.Team = team;
//                MainController.db.Projects.Add(proj);
//                MainController.db.SaveChanges();
//                MainController.db.Teams.Add(team);
//                MainController.db.SaveChanges();   
            }
            
            
                
            
            return View(model);
            
        }

        
        private async void SavePhoto(IFormFile file) 
        {
            if (file != null)
            {
                string path = "/img/" + file.FileName;

                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream("wwwroot" + path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
        }

        private async void SavePhoto(IFormFileCollection collection)
        {
            if (collection != null)
            {
                foreach (var item in collection)
                {
                    string path = "/img/" + item.FileName;

                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream("wwwroot" + path, FileMode.Create))
                    {
                        await item.CopyToAsync(fileStream);
                    }
                }
            }
        }
    }
}