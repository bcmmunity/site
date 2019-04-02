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
			optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=q31;Trusted_Connection=True;");
		}
		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			
			modelBuilder.Entity<ProjectUser>()
				.HasKey(bc => new { bc.Id, bc.ProjectId});  
			modelBuilder.Entity<ProjectUser>()
				.HasOne(bc => bc.Project)
				.WithMany(b => b.Members)
				.HasForeignKey(bc => bc.ProjectId);  
			modelBuilder.Entity<ProjectUser>()
				.HasOne(bc => bc.User)
				.WithMany(c => c.Projects)
				.HasForeignKey(bc => bc.Id);
			base.OnModelCreating(modelBuilder);
		}
		

		public DbSet<Article> Articles { get; set; }
		public DbSet<Project> Projects { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<Speciality> Specialities { get; set; }
		public DbSet<SN> SNs { get; set; }
	}
}