using api.eclipse.controle.projetos.Data.Contexts;
using api.eclipse.controle.projetos.Helpers;
using Microsoft.EntityFrameworkCore;

namespace api.eclipse.controle.projetos.Configuration
{
    public static class DatabaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var connectionString = ConnectionHelper.GetConnection(configuration);

            services.AddDbContext<EclipseContext>(options =>
            {
               options.UseSqlServer(connectionString);
                options.EnableSensitiveDataLogging();
            });
        }
    }
}
