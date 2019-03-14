using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace site.Models
{
	public class ApplicationContext : IdentityDbContext<User>
	{
		public ApplicationContext(DbContextOptions<ApplicationContext> options)
			: base(options)
		{
			Database.EnsureCreated();
		}

		public DbSet<Article> Articles { get; set; }
		public DbSet<Project> Projects { get; set; }
		public DbSet<Team> Teams { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<Speciality> Specialities { get; set; }
		public DbSet<Link> Links { get; set; }
		public DbSet<Social> Socials { get; set; }
		public DbSet<SN> SNs { get; set; }
		public DbSet<Experience> Experiences { get; set; }
		public DbSet<Learning> Learnings { get; set; }
	}
}