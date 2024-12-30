namespace api.eclipse.controle.projetos.Helpers
{
    public class ConnectionHelper
    {
        public static string GetConnection(IConfiguration configuration)
        {
            return configuration.GetConnectionString("DataBase");
        }
    }
}
