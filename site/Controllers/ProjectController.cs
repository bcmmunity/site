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
		public ApplicationContext _db;

		public ProjectController(ApplicationContext db)
        {
			_db = db;
        }

        public IActionResult Add()
        {
			ViewBag.Specialities = _db.Specialities.ToList();
			ViewBag.Users = _db.Users.ToList();


			return View();
        }

        [HttpPost]
        // TODO: Поправить стили в верстке
        public async Task<IActionResult> Add(ProjectViewModel model)
		{
			ViewBag.Specialities = _db.Specialities.ToList();
			ViewBag.Users = _db.Users.ToList();

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
                    string localPath = "/img/" + image.FileName;
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
                    Rang = _db.Projects.ToList().Count + 1,

                    SliderImages = paths,
                };
                List<Speciality> sps = new  List<Speciality>();
				if (model.Specs != null)
				{
					foreach (var id in model.Specs)
					{
						sps.Add(_db.Specialities.Find(id));
					}
				}

                Team team = new Team();
				if (model.Members != null)
				{
					foreach (var id in model.Members)
					{
						team.Members.Add(_db.Users.Find(id));
					}
				}
                proj.Specialities.AddRange(sps);
                proj.Team = team;
				if (model.Members != null)
				{
					foreach (var id in model.Members)
					{
						User user = _db.Users.Find(id);
						user.Projects.Add(proj);

						_db.Update(user);
						await _db.SaveChangesAsync();
					}
				}
				
				await _db.Projects.AddAsync(proj);
                await _db.SaveChangesAsync();   
                await _db.Teams.AddAsync(team);
                await _db.SaveChangesAsync();   
            }


            return RedirectToAction("About", "Home");
        }
        
        public ActionResult View(int id)
		{
			Project proj = _db.Projects.Find(id);
            if (proj != null)
            {
                return View("Project", proj);	
            }
            else
            {
                return View("Error");
            }
			
        }


    }
}