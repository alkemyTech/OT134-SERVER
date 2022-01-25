using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
	}

}
