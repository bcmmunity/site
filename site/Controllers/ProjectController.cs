using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Remotion.Linq.Clauses;
using site.Models;
using site.ViewModels;
using System.Security.Cryptography;
using System.Text;

namespace site.Controllers
{
    public class ProjectController : Controller
    {
		public ApplicationContext _db;

	    private IHostingEnvironment _contentPath;
	    
		public ProjectController(ApplicationContext db, IHostingEnvironment contentPath)
        {
			_db = db;
	        _contentPath = contentPath;
        }

        public IActionResult Add()
        {
			ViewBag.Specialities = _db.Specialities.ToList();
			ViewBag.Users = _db.Users.ToList();


			return View();
        }

        [HttpPost]
   
        public async Task<IActionResult> Add(ProjectViewModel model)
		{
			ViewBag.Specialities = _db.Specialities.ToList();
			ViewBag.Users = _db.Users.ToList();

			string path = "";
			string paths = "";
			string md5;
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
                    path =  model.Cover.FileName;
	                
	                using (MD5 md5Hash = MD5.Create())
	                {
		                md5 = GetMd5Hash(md5Hash, model.Title);	               
	                }
	                Directory.SetCurrentDirectory(_contentPath.WebRootPath + "/img/");
	                
	                if (!Directory.Exists("ProjectPhotos"))
		                Directory.CreateDirectory("ProjectPhotos");
	                
	                Directory.SetCurrentDirectory("ProjectPhotos");
	                Directory.CreateDirectory(md5);
	                Directory.SetCurrentDirectory(md5);
	                
	                
	                if (!Directory.Exists("Cover"))
		                Directory.CreateDirectory("Cover");
	                
	                Directory.SetCurrentDirectory("Cover");
	                
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await model.Cover.CopyToAsync(fileStream);
                    }
	                
	                Directory.SetCurrentDirectory(_contentPath.WebRootPath + "/img/ProjectPhotos");
                }
                else
                {
                    ViewBag.ImageError += "Загружено не изображение";
	                return View(model);
                }

                foreach (var image in model.SliderImages)
                {
                    if (!image.ContentType.StartsWith("image"))
                    {
                        ViewBag.ImageError += " Не все загруженные фаилы являются изображениями";
                        return View(model);
                    }
                }
	            
	            Directory.SetCurrentDirectory(md5);
	            
	            if (!Directory.Exists("Slider"))
		            Directory.CreateDirectory("Slider");
	                
	            Directory.SetCurrentDirectory("Slider");
	            
                foreach (var image in model.SliderImages)
                {
                    string localPath = $"/img/ProjectPhotos/{md5}/Slider/{image.FileName}";
	                paths += localPath + ":" ;
	                
	                
	                
	                
                    using (var fileStream = new FileStream(image.FileName, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }
	                
	                
                }
				
	            Directory.SetCurrentDirectory(_contentPath.ContentRootPath);
	            
	            paths = paths.Substring(0, paths.Length - 1);
	  
	            

                Project proj = new Project
                {
                    Name = model.Title,
                    Img = $"/img/ProjectPhotos/{md5}/Cover/{path}",
                    Description = model.Description,
                    Rang = _db.Projects.ToList().Count + 1
                    

                };
	            proj.SliderImages = paths;
	            List<Speciality> sps = new  List<Speciality>();
	            if (model.Specs != null)
	            {
		            foreach (var id in model.Specs)
		            {
			            sps.Add(_db.Specialities.Find(id));
		            }
	            }
	            proj.Specialities.AddRange(sps);
	            _db.Projects.Add(proj);     
	                
                
				if (model.Members != null)
				{
					foreach (var id in model.Members)
					{
						User user = _db.Users.Find(id);


						user.Projects.Add(new ProjectUser()
						{
							ProjectId = proj.ProjectId,
							Id = id
						});

						
					}
				}
	            _db.SaveChanges();
	            
				
            }


            return RedirectToAction("About", "Home");
        }
        
        public ActionResult View(int id)
		{
			Project proj = _db.Projects
				.Include(t => t.Members)
				.ThenInclude(u => u.User)
				.FirstOrDefault(t => t.ProjectId == id);
			Console.WriteLine("\n\n\n\n\n\n\n");
			Console.WriteLine(proj.SliderImages);
			ViewBag.Sliders = proj.SliderImages.Split(":");
			
			if (proj != null)
			{

            
                return View("Project", proj);	
            }
            else
            {
                return View("Error");
            }
			
        }
	    
	    static string GetMd5Hash(MD5 md5Hash, string input)
	    {
			// https://docs.microsoft.com/ru-ru/dotnet/api/system.security.cryptography.md5?view=netframework-4.7.2
		    byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

		    StringBuilder sBuilder = new StringBuilder();

		    for (int i = 0; i < data.Length; i++)
		    {
			    sBuilder.Append(data[i].ToString("x2"));
		    }
		    return sBuilder.ToString();
	    }


    }
}