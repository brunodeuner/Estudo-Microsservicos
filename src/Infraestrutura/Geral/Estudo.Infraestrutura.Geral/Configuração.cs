using Microsoft.Extensions.Configuration;

namespace Estudo.Infraestrutura.Geral
{
    public static class Configuração
    {
        private const string AppSettingsJson = "appsettings.json";

        public static IConfiguration CriarConfiguraçãoLendoOAppsettings() =>
            new ConfigurationBuilder().AddJsonFile(AppSettingsJson).Build();
    }
}
