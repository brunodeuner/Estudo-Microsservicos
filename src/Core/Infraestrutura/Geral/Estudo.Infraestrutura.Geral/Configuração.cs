using Microsoft.Extensions.Configuration;

namespace Estudo.Core.Infraestrutura.Geral
{
    public static class Configuração
    {
        private const string AppSettingsJson = "appsettings.json";

        public static IConfiguration CriarConfiguraçãoLendoOAppsettings() =>
            new ConfigurationBuilder().AddJsonFile(AppSettingsJson).Build();
    }
}
