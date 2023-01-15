using Microsoft.EntityFrameworkCore;
using PizzaApp.Server.DAL;

namespace PizzaApp.Server.Extensions
{
    public static class DatabaseServiceExtension
    {
        /// <summary>
        /// add mysql database connection to program
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureMySQLDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            // add database
            string connectionString = configuration.GetConnectionString("PizzaConnection");
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
            services.AddDatabaseDeveloperPageExceptionFilter();
        }
    }
}
