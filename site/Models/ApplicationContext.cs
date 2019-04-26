using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace site.Models
{
	public class ApplicationContext : IdentityDbContext<User>
	{
		public ApplicationContext(DbContextOptions<ApplicationContext> options)
			: base(options)
		{
//			Database.Migrate();
			Database.EnsureCreated();
			
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			
			optionsBuilder.UseSqlServer("Server=localhost;Database=u0641156_diffind;User Id = u0641156_diffind; Password = Qwartet123!");
//			optionsBuilder.UseSqlServer("Server=mssql.u0641156.plsk.regruhosting.ru;Database=u0641156_diffind;User Id = u0641156_diffind; Password = Qwartet123!");
//			optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=q112;Trusted_Connection=True;");
//			optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=a12;Trusted_Connection=True;");
		}
		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			string[] snNames =
			{
				"twitter",
				"facebook",
				"vk",
				"instagram",
				"whatsapp",
				"behance",
				"github",
				"youtube"
			};
			for (int i = 0; i < snNames.Length; i++)
			{
				SN tag = new SN
				{
					SNId = i + 1,
					Title = snNames[i],
					Pic = $"../../img/{snNames[i].ToLower()}.svg"
				};
				modelBuilder.Entity<SN>().HasData(tag);
			}
			
			
			
			
			modelBuilder.Entity<ProjectUser>()
				.HasKey(bc => new { bc.Id, bc.ProjectId});
			modelBuilder.Entity<ProjectUser>()
				.HasOne(bc => bc.Project)
				.WithMany(b => b.Members)
				.HasForeignKey(bc => bc.ProjectId);
			modelBuilder.Entity<ProjectUser>()
				.HasOne(bc => bc.User)
				.WithMany(bc => bc.Projects)
				.HasForeignKey(bc => bc.Id);

			
			modelBuilder.Entity<UserSpec>()
				.HasKey(bc => new { bc.Id, bc.SpecialityId});  
			
			modelBuilder.Entity<UserSpec>()
				.HasOne(bc => bc.User)
				.WithMany(bc => bc.Specialities)
				.HasForeignKey(bc => bc.Id);  
			modelBuilder.Entity<UserSpec>()
				.HasOne(bc => bc.Speciality)
				.WithMany(c => c.Users)
				.HasForeignKey(bc => bc.SpecialityId);
			
			modelBuilder.Entity<ProjectSpec>()
				.HasKey(bc => new { bc.ProjectId, bc.SpecialityId});  
			
			modelBuilder.Entity<ProjectSpec>()
				.HasOne(bc => bc.Project)
				.WithMany(bc => bc.Specialities)
				.HasForeignKey(bc => bc.ProjectId);  
			modelBuilder.Entity<ProjectSpec>()
				.HasOne(bc => bc.Speciality)
				.WithMany(c => c.Projects)
				.HasForeignKey(bc => bc.SpecialityId);
			
			base.OnModelCreating(modelBuilder);
		}
		

		public DbSet<Article> Articles { get; set; }
		public DbSet<Project> Projects { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<Speciality> Specialities { get; set; }
		public DbSet<SN> SNs { get; set; }
	}
}