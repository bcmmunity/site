using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
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
using Microsoft.AspNetCore.Identity;

namespace site.Controllers
{
    public class ProjectController : Controller
    {
		public ApplicationContext _db;

	    private IHostingEnvironment _contentPath;

	    private readonly UserManager<User> _userManager;
	    
		public ProjectController(UserManager<User> userManager, ApplicationContext db, IHostingEnvironment contentPath)
        {
			_db = db;
	        _contentPath = contentPath;
	        _userManager = userManager;
        }

        public IActionResult Add()
        {
//	        // TODO: РОЛИ РОЛИ РОЛИ
//	        string nikita = _userManager.GetUserId(HttpContext.User);
//	        ViewBag.nikita = nikita;
//	        if (nikita != "87759cdf-3b58-483b-a738-f79a051bac23" && nikita != "4d980ae0-2592-43fa-a2c1-35f8789102b7")
//	        {
//		        return NotFound();
//	        }
	        
			ViewBag.Specialities = _db.Specialities.ToList();
			ViewBag.Users = _db.Users.ToList();
			

			return View();
        }

        [HttpPost]
        
        public async Task<IActionResult> Add(ProjectViewModel model)
		{
			ViewBag.Specialities = _db.Specialities.ToList();
			ViewBag.Users = _db.Users.ToList();
//			// TODO: РОЛИ РОЛИ РОЛИ
//			string nikita = _userManager.GetUserId(HttpContext.User);
//			ViewBag.nikita = nikita;
//			if (nikita != "87759cdf-3b58-483b-a738-f79a051bac23" && nikita != "4d980ae0-2592-43fa-a2c1-35f8789102b7")
//			{
//				return NotFound();
//			}
			
			
			string path = "";
			string paths = "";
			string uniqueId = Guid.NewGuid().ToString();
			if (ModelState.IsValid)
            {

	            #region Сохрание фотографии
	            
                if (model.Cover == null || model.SliderImages == null)
                {
                    ViewBag.ImageError += " Изображение(я) не загружено(ы)";
                    return View(model);
                }

                
                
                if (model.Cover.ContentType.StartsWith("image"))
                {
                    path =  Guid.NewGuid().ToString();
                 
	                Directory.SetCurrentDirectory(_contentPath.WebRootPath + "/img/");
	                
	                if (!Directory.Exists("ProjectPhotos"))
		                Directory.CreateDirectory("ProjectPhotos");
	                
	                Directory.SetCurrentDirectory("ProjectPhotos");
	                Directory.CreateDirectory(uniqueId);
	                Directory.SetCurrentDirectory(uniqueId);
	                
	                
	                if (!Directory.Exists("Cover"))
		                Directory.CreateDirectory("Cover");
	                
	                Directory.SetCurrentDirectory("Cover");
	                
                    using (var fileStream = model.Cover.OpenReadStream())
                    {
	                    Image img = Image.FromStream(fileStream);
	                    using (var resized = ImageUtilities.ResizeImage(img , img.Width, img.Height))
	                    {
		                    ImageUtilities.SaveJpeg($"{path}.jpeg", resized, 80);
	                    }
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
	            
	            Directory.SetCurrentDirectory(uniqueId);
	            
	            if (!Directory.Exists("Slider"))
		            Directory.CreateDirectory("Slider");
	                
	            Directory.SetCurrentDirectory("Slider");
	            
                foreach (var image in model.SliderImages)
                {
	                string sliderImagePath = Guid.NewGuid().ToString();
                    string localPath = $"/img/ProjectPhotos/{uniqueId}/Slider/{sliderImagePath}.jpeg";
	                
                    paths += localPath + ":" ;
					
                    using (var fileStream = image.OpenReadStream())
                    {
	                    Image img = Image.FromStream(fileStream);
	                    using (var resized = ImageUtilities.ResizeImage(img , img.Width, img.Height))
	                    {
		                    ImageUtilities.SaveJpeg($"{sliderImagePath}.jpeg", resized, 80);
	                    }
                    }
	                
	                
                }
				
	            Directory.SetCurrentDirectory(_contentPath.ContentRootPath);
	            
	            paths = paths.Substring(0, paths.Length - 1);
				
	            #endregion
	            

                Project proj = new Project
                {
                    Name = model.Title,
                    Img = $"/img/ProjectPhotos/{uniqueId}/Cover/{path}.jpeg",
                    Description = model.Description,
                    SliderImages = paths,
                    Rang = _db.Projects.ToList().Count + 1
                };
                
                await _db.Projects.AddAsync(proj);
                await _db.SaveChangesAsync();

                #region Добавление ссылок
				
                foreach (var link in model.Links)
                {
	                Link lnk = new Link
	                {
						Href = link
	                };
	                
	                proj.Links.Add(lnk);
                }
                await _db.SaveChangesAsync();
                #endregion
                     
                #region Добавление технологии
            
                if (model.Specialities != null)
                {
	                foreach (var sp in model.Specialities)
	                {
		                proj.Specialities.Add(new ProjectSpec
		                {
			                SpecialityId = sp,
			                ProjectId = proj.ProjectId
		                });
	                }
                } 

                await _db.SaveChangesAsync(); 
                #endregion

                #region Добавление лидера

                if (model.Leader != null)
                {
	                User user = _db.Users.Find(model.Leader);
	                proj.Leader = user;
	                
                }
                await _db.SaveChangesAsync(); 
                #endregion
                
                
	            #region Добавление участников
	                        
				if (model.Members != null)
				{
					if (!model.Members.Contains(model.Leader))
					{
						model.Members.Add(model.Leader);
					}
					foreach (var id in model.Members)
					{
						User user = _db.Users.Find(id);
						bool lead = user.Id == model.Leader;
						
						user.Projects.Add(new ProjectUser
						{
							ProjectId = proj.ProjectId,
							IsLeader = lead,
							Id = id
						});					
					}
				}
				await _db.SaveChangesAsync();  
				
				#endregion
            }
            return RedirectToAction("About", "Home");
        }

        public async Task<IActionResult> Edit(int id)
        {
	        // TODO: РОЛИ РОЛИ РОЛИ
//	        string nikita = _userManager.GetUserId(HttpContext.User);
//	        ViewBag.nikita = nikita;
//	        if (nikita != "87759cdf-3b58-483b-a738-f79a051bac23" && nikita != "4d980ae0-2592-43fa-a2c1-35f8789102b7")
//	        {
//		        return NotFound();
//	        }

	        Project project = await _db.Projects
		        .Include(p => p.Links)
		        .Include(l => l.Leader)
		        .Include(p => p.Members)
		        .Include(p => p.Specialities)
		        .FirstOrDefaultAsync(p => p.ProjectId == id);
		        
	        if (project == null)
	        {
		        return NotFound();
	        }        
	        
	        EditProjectViewModel model = new EditProjectViewModel
	        {
				Id = project.ProjectId,
				Name = project.Name,
				ProjectUId = project.Img.Split("/")[3],
				Description = project.Description,
				Specialities = project.Specialities.Select(u => u.SpecialityId).ToList(),
				Members = project.Members.Select(u => u.Id).ToList(),
				Links =  project.Links.Select(u => u.Href).ToList(),
				SliderImagesPreview = project.SliderImages.Split(":").ToList(),
				CoverPath = project.Img,
				Leader = project.Leader.Id
	        };
			
	        ViewBag.Specialities = _db.Specialities.ToList();
	        ViewBag.Members = _db.Users.ToList();
	        return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(EditProjectViewModel model)
        {
	        // TODO: РОЛИ РОЛИ РОЛИ
//	        string nikita = _userManager.GetUserId(HttpContext.User);
//	        ViewBag.nikita = nikita;
//	        if (nikita != "87759cdf-3b58-483b-a738-f79a051bac23" && nikita != "4d980ae0-2592-43fa-a2c1-35f8789102b7")
//	        {
//		        return NotFound();
//	        }
	        string path = "";
	        string uniqueId = Guid.NewGuid().ToString();
	        
	        if (ModelState.IsValid)
	        {
		        Project project = _db.Projects
			        .Include(p => p.Links)
			        .Include(p => p.Members)
			        .Include(l => l.Leader)
			        .Include(p => p.Specialities)
			        .FirstOrDefault(p => p.ProjectId == model.Id);

		        List<int> projectSpecialities = project?.Specialities.Select(u => u.SpecialityId).ToList();
		        
		        List<string> projectLinks = project?.Links.Select(h => h.Href).ToList();

		        List<string> sliderImages = project?.SliderImages.Split(":").ToList();
		        
		        
		        
		        
		        project.Name = model.Name;
		        project.Description = model.Description;
		        
		        #region Изменение обложки проекта
		        if (model.NewCover != null && model.NewCover.ContentType.StartsWith("image"))
		        {
			        path =  Guid.NewGuid().ToString();
                 
			        Directory.SetCurrentDirectory(_contentPath.WebRootPath + "/img/");
	                
			        if (!Directory.Exists("ProjectPhotos"))
				        Directory.CreateDirectory("ProjectPhotos");
	                
			        Directory.SetCurrentDirectory("ProjectPhotos");
			        Directory.SetCurrentDirectory(model.ProjectUId);
	                
			        if (!Directory.Exists("Cover"))
				        Directory.CreateDirectory("Cover");
	                
			        Directory.SetCurrentDirectory("Cover");
	                
			        using (var fileStream = model.NewCover.OpenReadStream())
			        {
				        Image img = Image.FromStream(fileStream);
				        using (var resized = ImageUtilities.ResizeImage(img , img.Width, img.Height))
				        {
					        ImageUtilities.SaveJpeg($"{path}.jpeg", resized, 80);
				        }
			        }
			        project.Img = $"/img/ProjectPhotos/{model.ProjectUId}/Cover/{path}.jpeg";
	                
			        Directory.SetCurrentDirectory(_contentPath.WebRootPath + "/img/ProjectPhotos");
		        }

		        

		        await _db.SaveChangesAsync();

		       #endregion
		       
		       #region Изменение фотографии для слайдера

		       if (model.OldSliderImages != null)
		       {
			       var difference = Utils.Compare(sliderImages, model.OldSliderImages);
			       List<string> toDelete = difference[0];
			       foreach (var id in toDelete)
			       {
				       sliderImages.Remove(id);
			       }

			       project.SliderImages = string.Join(":", sliderImages);

			       await _db.SaveChangesAsync();
			       
			       if (model.SliderImages == null)
				       project.SliderImages = string.Join(":", sliderImages);
			       
		       }
		       
		       Directory.SetCurrentDirectory(_contentPath.WebRootPath + "/img/ProjectPhotos");


	           
		       Directory.SetCurrentDirectory(model.ProjectUId);

	            
		       if (!Directory.Exists("Slider"))
			       Directory.CreateDirectory("Slider");
	                
		       Directory.SetCurrentDirectory("Slider");
		       if (model.SliderImages != null)
		       {
			       foreach (var image in model.SliderImages)
			       {
				       if (image.ContentType.StartsWith("image"))
				       {
				       
					       string sliderImagePath = Guid.NewGuid().ToString();
					       string localPath = $"/img/ProjectPhotos/{model.ProjectUId}/Slider/{sliderImagePath}.jpeg";
					       sliderImages.Add(localPath);
					
					       using (var fileStream = image.OpenReadStream())
					       {
						       Image img = Image.FromStream(fileStream);
						       using (var resized = ImageUtilities.ResizeImage(img , img.Width, img.Height))
						       {
							       ImageUtilities.SaveJpeg($"{sliderImagePath}.jpeg", resized, 80);
						       }
					       }
				       }
				       project.SliderImages = string.Join(":", sliderImages);

			       
	                
	                
			       }
		       }
		       
				
		       Directory.SetCurrentDirectory(_contentPath.ContentRootPath);
	            
		       
		       
		       
		       
		       
		       #endregion
		        
		        
		        
		        #region Изменение технологии 
		        
		        if (model.Specialities != null)
		        {
			        var specIdList = Utils.Compare(projectSpecialities, model.Specialities);
			        List<int> toDelete = specIdList[0];
			        List<int> toAdd = specIdList[1];
			        foreach (var id in toDelete)
			        {
				        project.Specialities.Remove(project.Specialities.FirstOrDefault(u => u.SpecialityId == id));
			        }
			        foreach (var id in toAdd)
			        {
				        if (!projectSpecialities.Contains(id))
				        {
					        project.Specialities.Add(new ProjectSpec
					        {
						        SpecialityId = id,
						        ProjectId = model.Id
					        });
				        }
			        }
		        }
		        
		        await _db.SaveChangesAsync();
		        
		        #endregion
		        
		        #region Изменение лидера

		        if (model.Leader != null)
		        {
			        User leader = _db.Users.Find(model.Leader);
			        project.Leader = leader;
			        await _db.SaveChangesAsync();
		        }
		        #endregion
		        
		        
		        #region Изменение пользователей
		        
		        if (model.Members != null)
		        {
			        List<string> projectUsers = project.Members.Select(u => u.Id).ToList();
			        var userIdList = Utils.Compare(projectUsers, model.Members);
			        List<string> toDelete = userIdList[0];
			        List<string> toAdd = userIdList[1];
			        bool lead;
			        foreach (var id in toDelete)
			        {
				        User user = _db.Users.Find(id);
				        user.Projects.Remove(user.Projects.FirstOrDefault(u => u.Id == id));
				        projectUsers.Remove(id);
			        }
			        if (!toAdd.Contains(model.Leader))
						toAdd.Add(model.Leader);
			        foreach (var id in toAdd)
			        {
				        if (!projectUsers.Contains(id))
				        {
					        User user = _db.Users.Find(id);
					        lead = id == model.Leader;
					        user.Projects.Add(new ProjectUser
					        {
						        Id = id,
						        IsLeader = lead,
						        ProjectId = model.Id
					        });
				        }
			        }
		        }

		        await _db.SaveChangesAsync();

		        #endregion
		        
		        #region Изменение ссылок

		        if (model.Links != null)
		        {
			        var linksList = Utils.Compare(projectLinks, model.Links);
			        List<string> toDelete = linksList[0];
			        
			        List<string> toAdd = linksList[1];
			        foreach (var href in toDelete)
			        {
				        project.Links.Remove(project.Links.FirstOrDefault(u => u.Href == href));
			        }
			        foreach (var href in toAdd)
			        {
				        if (projectLinks != null && !projectLinks.Contains(href) && href != "")
				        {
					        project.Links.Add(new Link
					        {
						        Href = href
					        });
				        }
			        }
		        }
		        #endregion

		        await _db.SaveChangesAsync();
		        return RedirectToAction("Index", "Home");
	        }
	        return View("Error");
        }
        
        
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
	        // TODO: РОЛИ РОЛИ РОЛИ
//	        string nikita = _userManager.GetUserId(HttpContext.User);
//	        ViewBag.nikita = nikita;
//	        if (nikita != "87759cdf-3b58-483b-a738-f79a051bac23" && nikita != "4d980ae0-2592-43fa-a2c1-35f8789102b7")
//	        {
//		        return NotFound();
//	        }
	        
	        
	        Project proj = await _db.Projects
		        .Include(l => l.Links)
		        .FirstOrDefaultAsync(p => p.ProjectId == id);
			
	        
				
	        
	        if (proj != null)
	        {
				_db.Projects.Remove(proj);
				await _db.SaveChangesAsync();
	        }

	        return RedirectToAction("Index", "Home");
        }
        
        public ActionResult View(int? id)
		{
			
			string nikita = _userManager.GetUserId(HttpContext.User);
			ViewBag.nikita = nikita;
			if (id == null)
			{
				return NotFound();
			}
			
			Project proj = _db.Projects
				.Include(l => l.Leader)
				.Include(t => t.Members)
				.ThenInclude(u => u.User)
				.ThenInclude(l => l.Links)
				.Include(s => s.Specialities)
				.ThenInclude(s => s.Speciality)
				.Include(l => l.Links)
				.FirstOrDefault(t => t.ProjectId == id);

			if (!proj.IsShowed)
			{
				return NotFound();
			}
			
			if (proj != null)
			{
                return View("Project", proj);	
            }
            
			return View("Error");
			
        }

        public IActionResult List()
        {
	        List<Project> projects = _db.Projects.ToList();
	        return View(projects);
        }

        public async Task<IActionResult> ChangeVisibility(int id)
        {
	        Project project = _db.Projects.Find(id);
	        project.IsShowed = !project.IsShowed;
	        _db.Projects.Update(project);
	        await _db.SaveChangesAsync();
	        return RedirectToAction("List");
        }
        
	 
	    
	  


    }
}