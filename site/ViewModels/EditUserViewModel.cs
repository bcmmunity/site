using System;
using site.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace site.ViewModels
{
	public class EditUserViewModel
	{
		public string Id { get; set; }
		public string Email { get; set; }
		public IFormFile Photo { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Position { get; set; }
		public string Description { get; set; }
		
		public int CropX { get; set; }
		public int CropY { get; set; }
		public int CropWidth { get; set; }
		public int CropHeight { get; set; }
		
		public List<string> Titles { get; set; }
		public List<string> Links { get; set; }
		public List<string> Descriptions { get; set; }
		public List<DateTime> StartDates { get; set; }
		public List<DateTime> FinishDates { get; set; }
		public List<bool> IsWorks { get; set; }
		public List<bool> Nows { get; set; }
		public List<string> Socials { get; set; }
	}
}