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

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
//			optionsBuilder.UseSqlServer("Server=localhost;Database=u0641156_diffind;User Id = u0641156_diffind; Password = Qwartet123!");
//			optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=q112;Trusted_Connection=True;");
			optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=h35;Trusted_Connection=True;");
		}

		public DbSet<Article> Articles { get; set; }
		public DbSet<Project> Projects { get; set; }
		public DbSet<Team> Teams { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<Speciality> Specialities { get; set; }
//		public DbSet<Link> Links { get; set; }
//		public DbSet<Social> Socials { get; set; }
		public DbSet<SN> SNs { get; set; }
//		public DbSet<Experience> Experiences { get; set; }
	}
}