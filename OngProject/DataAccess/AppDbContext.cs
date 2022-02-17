using Microsoft.EntityFrameworkCore;
using OngProject.Entities;

namespace OngProject.DataAccess
{
    public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<Activities> Activities { get; set; }
		public DbSet<Category> Categories { get; set; }

		public DbSet<Comment> Comments { get; set; }

		public DbSet<Contacts> Contacts { get; set; }

		public DbSet<Member> Members { get; set; }

		public DbSet<New> News { get; set; }

		public DbSet<Organization> Organizations { get; set; }

		public DbSet<Rol> Roles { get; set; }

		public DbSet<Slides> Slides { get; set; }

		public DbSet<Testimonials> Testimonials { get; set; }

		public DbSet<User> Users { get; set; }


		SeedDataUser dataUser = new SeedDataUser();
		SeedUserRol rolUser = new SeedUserRol();
		SeedTestimonial seedTestimonial = new SeedTestimonial();
		SeedNew seedNew = new SeedNew();
		SeedCategory seedCategory = new SeedCategory();
		SeedActivity seedActivity = new SeedActivity();
		SeedMember seedMember = new SeedMember();
		SeedContact seedContact = new SeedContact();
		SeedComment seedComment = new SeedComment();
		SeedOrganization seedOrganization = new SeedOrganization();
		SeedSlide seedSlide = new SeedSlide();

		
		SeedDataUser DataUser = new SeedDataUser();
		SeedUserRol RolUser = new SeedUserRol();
		SeedDataMember DataMember = new SeedDataMember();
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			this.SeedCategories(modelBuilder);
			this.SeedActivities(modelBuilder);
			RolUser.SeedRoles(modelBuilder);
			DataUser.SeedRegularUsers(modelBuilder);
			DataUser.SeedAdministratorUsers(modelBuilder);
			DataMember.SeedMembers(modelBuilder);
		}


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			seedCategory.SeedCategories(modelBuilder);
			seedActivity.SeedActivities(modelBuilder);
			rolUser.SeedRoles(modelBuilder);
			dataUser.SeedRegularUsers(modelBuilder);
			dataUser.SeedAdministratorUsers(modelBuilder);
			seedTestimonial.SeedTestimonials(modelBuilder);
			seedNew.SeedNews(modelBuilder);
			seedMember.SeedMembers(modelBuilder);
			seedComment.SeedComments(modelBuilder);
			seedContact.SeedContacts(modelBuilder);
			seedOrganization.SeedOrganizations(modelBuilder);
			seedSlide.SeedSlides(modelBuilder);
		}
	}	
}