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
	}
}