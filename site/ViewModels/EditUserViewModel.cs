using System;
using site.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace site.ViewModels
{
	public class EditUserViewModel
	{
		public string Id { get; set; }
		public string Email { get; set; }
		public string Photo { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Position { get; set; }
		public string Description { get; set; }
		
		public List<string> Titles { get; set; }
		public List<string> Links { get; set; }
		public List<string> Descriptions { get; set; }
		public List<DateTime> StartDates { get; set; }
		public List<DateTime> FinishDates { get; set; }
		public List<bool> IsWorks { get; set; }
		public List<string> Socials { get; set; }
	}
}