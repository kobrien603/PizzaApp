using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PizzaApp.Server.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace PizzaApp.Server
{
    public static class Startup
    {
        /// <summary>
        /// used to help inject DI projects from PizzaApp to any web or app tool that connects to it
        /// ex: in PizzaApp.Web we add builder.Services.TryAddPizzaAppRCL();
        /// </summary>
        public static void TryAndAddPizzaServer(this IServiceCollection services)
        {
			// add database
			//builder.Configuration.GetConnectionString("PizzaConnection")
			string connectionString = "server=127.0.0.1;port=3306;user=pizzaadmin;password=&Naa4F4uRT2F@Wu5Xu^dLz!5m!Z5DB;database=pizza;";
			services.AddDatabaseDeveloperPageExceptionFilter();
			services.AddDbContextFactory<PizzaContext>(options =>
			{
				options.UseMySql(
					connectionString,
					ServerVersion.AutoDetect(connectionString),
					x => x.MigrationsAssembly("PizzaApp.Server")
				);
			});

			services.AddDbContext<PizzaContext>(options =>
			{
				options.UseMySql(
					connectionString,
					ServerVersion.AutoDetect(connectionString),
					x => x.MigrationsAssembly("PizzaApp.Server")
				);
			});

			services.AddEntityFrameworkMySql();
			//new EntityFrameworkRelationalDesignServicesBuilder(builder.Services).TryAddCoreServices();
		}
	}
}
