using Microsoft.Extensions.DependencyInjection;

namespace api.eclipse.controle.projetos.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            RegisterServicesApplication(services);
            RegisterServicesInfraData(services);
        }
        /// <summary>
        /// Application 
        /// </summary>
        /// <param name="services"></param>
        private static void RegisterServicesApplication(IServiceCollection services)
        {
            //services.AddScoped<IMotoristaService, MotoristaService>();


        }

        /// <summary>
        /// Infra - Data 
        /// </summary>
        /// <param name="services"></param>
        private static void RegisterServicesInfraData(IServiceCollection services)
        {

            //services.AddScoped<IMotoristaRepository, MotoristaRepository>();
            //services.AddScoped<IColaboradorService, ColaboradorService>();

        }
    }
}
