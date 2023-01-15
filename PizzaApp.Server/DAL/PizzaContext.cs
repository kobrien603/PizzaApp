using Microsoft.EntityFrameworkCore;
using PizzaApp.Server.DAL.Models;
using PizzaApp.Server.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PizzaApp.Server.DAL
{
    public class PizzaContext : DbContext
    {
        public PizzaContext(DbContextOptions<PizzaContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
		public DbSet<Company> Companies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Role> Roles { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users")
                .HasQueryFilter(x => !x.IsDeleted)
                .HasMany(u => u.Role)
                .WithMany(r => r.User);
			modelBuilder.Entity<Company>().ToTable("company");
            modelBuilder.Entity<Category>().ToTable("categories");
            modelBuilder.Entity<Item>().ToTable("items");
            modelBuilder.Entity<Role>().ToTable("roles")
                .Property(e => e.UserRole)
                .HasConversion(
                    v => (int)v,
                    v => (UserRoles)v);

            // push user roles to database
            var enumType = typeof(UserRoles);
            var enumValues = Enum.GetValues(enumType).Cast<UserRoles>();
            var myEntities = enumValues.Select((value, index) => new Role { ID = index + 1, Name = value.ToString(), UserRole = value });
            modelBuilder.Entity<Role>().HasData(myEntities); // assign back
        }
    }
}
