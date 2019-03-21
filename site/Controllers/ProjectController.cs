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
            string path = "";
            List<string> paths = new List<string>();
            if (ModelState.IsValid)
            {
                Console.WriteLine(model.Cover.ContentType);

                if (model.Cover == null || model.SliderImages == null)
                {
                    ViewBag.ImageError += " Изображение(я) не загружено(ы)";
                    return View(model);
                }

                if (model.Cover.ContentType.StartsWith("image"))
                {
                    path = "/img/" + model.Cover.FileName;

                    using (var fileStream = new FileStream("wwwroot" + path, FileMode.Create))
                    {
                        await model.Cover.CopyToAsync(fileStream);
                    }
                }
                else
                {
                    ViewBag.ImageError += "Загружено не изображение";
                }

                foreach (var image in model.SliderImages)
                {
                    if (!image.ContentType.StartsWith("image"))
                    {
                        ViewBag.ImageError += " Не все загруженные фаилы являются изображениями";
                        return View(model);
                    }
                }
                
                foreach (var image in model.SliderImages)
                {
                    string localPath = "/img/" + model.Cover.FileName;
                    paths.Add(localPath);
                    using (var fileStream = new FileStream("wwwroot" + localPath, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }
                }
                
               
                Project proj = new Project
                {
                    Name = model.Title,
                    Img = path,
                    Description = model.Description,
                    Rang = MainController.db.Projects.ToList().Count + 1,

                    SliderImages = paths,
                };
                List<Speciality> sps = new  List<Speciality>();
                foreach (var id in model.Specs)
                {
                    sps.Add(MainController.db.Specialities.Find(id));
                }
                Team team = new Team();
                foreach (var id in model.Members)
                {
                    team.Members.Add(MainController.db.Users.Find(id));
                }
                proj.Specialities.AddRange(sps);
                proj.Team = team;
                await MainController.db.Projects.AddAsync(proj);
                await MainController.db.Teams.AddAsync(team);
                MainController.db.SaveChanges();   
            }


            return View(model);
        }


    }
}