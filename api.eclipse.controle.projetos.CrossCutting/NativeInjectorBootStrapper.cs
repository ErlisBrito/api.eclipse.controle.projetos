using api.eclipse.controle.projetos.Application.Interfaces;
using api.eclipse.controle.projetos.Application.Services;
using api.eclipse.controle.projetos.Data.Repository;
using api.eclipse.controle.projetos.Domain.Interfaces;
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
            services.AddScoped<IProjetosAppServices, ProjetosAppServices>();
            services.AddScoped<ITarefasAppServices, TarefasAppServices>();
            services.AddScoped<IHistoricoTarefaAppServices, HistoricoTarefaAppServices>();
        }

        /// <summary>
        /// Infra - Data 
        /// </summary>
        /// <param name="services"></param>
        private static void RegisterServicesInfraData(IServiceCollection services)
        {
            services.AddScoped<IProjetosRepository, ProjetosRepository>();
            services.AddScoped<ITarefaRepository, TarefaRepository>();
            services.AddScoped<IHistoricoTarefaRepository, HistoricoTarefaRepository>();

        }
    }
}
