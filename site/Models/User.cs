using Microsoft.AspNetCore.Identity;

namespace site.Models
{
	public class User : IdentityUser
	{
		public string photo { get; set; }
	}
}
