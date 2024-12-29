using api.eclipse.controle.projetos.CrossCutting.IoC;

namespace api.eclipse.controle.projetos.Configuration
{
    public static class DependencyInjectonConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentException(null, nameof(services));
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
