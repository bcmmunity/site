﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace site.Models
{
	public class User : IdentityUser
	{
		//[Key]
		//[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
		public string Photo { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Position { get; set; }
		public string Description { get; set; }
		public int Rang { get; set; }
		public bool IsShowed { get; set; }
		[NotMapped]
		public List<Speciality> Specialities { get; set; }
		[NotMapped]
		public List<Project> Projects { get; set; }
		public List<Link> Links { get; set; }
		[NotMapped]
		public List<Experience> Experiences { get; set; }
		[NotMapped]
		public List<Learning> Learnings { get; set; }

		public User()
		{
			Learnings = new List<Learning>();
			Experiences = new List<Experience>();
			Specialities = new List<Speciality>();
			Links = new List<Link>();
			Projects = new List<Project>();
		}
	}
}
