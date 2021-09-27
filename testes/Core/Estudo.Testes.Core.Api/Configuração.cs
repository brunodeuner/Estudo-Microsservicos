using Microsoft.Extensions.Configuration;

namespace Estudo.Testes.Core.Api
{
    internal static class Configuração
    {
        private const string AppSettingsJson = "appsettings.json";

        public static IConfiguration Criar() => new ConfigurationBuilder().AddJsonFile(AppSettingsJson).Build();
    }
}
