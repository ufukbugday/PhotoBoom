using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PhotoBoom.Entity;

namespace PhotoBoom.DataAccess
{
	public class PhotoBoomDbContext : DbContext
	{
		public PhotoBoomDbContext()
		{
		}
		public PhotoBoomDbContext(DbContextOptions options) : base(options)
		{
		}
		protected override void OnConfiguring(DbContextOptionsBuilder options)
			=> options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=PhotoBoomDB; Trusted_Connection=true");
		public DbSet<Photo> Photos { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Photo>().HasData(
				new Photo
				{
					Id = 1,
					Title = "Title",
					Tag =  "Tag",
					PhotoPath = "PhotoPath"
				});
		}
	}
}
