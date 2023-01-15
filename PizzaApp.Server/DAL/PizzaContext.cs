using Microsoft.EntityFrameworkCore;
using PizzaApp.Server.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(x => !x.IsDeleted).ToTable("users");
			modelBuilder.Entity<Company>().ToTable("company");
            modelBuilder.Entity<Category>().ToTable("categories");
            modelBuilder.Entity<Item>().ToTable("items");
        }
    }
}
