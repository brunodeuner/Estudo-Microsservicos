using Estudo.Cobranças.Aplicação.Armazenamento.Ravendb;
using Estudo.Core.Infraestrutura.Armazenamento.Ravendb;
using Estudo.Core.Infraestrutura.Geral;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Estudo.Cobranças.Serviço.InicializaçãoDoBancoDeDados
{
    public static class Program
    {
        public static async Task Main()
        {
            var configuração = Configuração.CriarConfiguraçãoLendoOAppsettings();
            var configuraçãoDoRavendb = configuração.GetSection(nameof(ConfiguraçãoDoRavendb)).Get<ConfiguraçãoDoRavendb>();
            var fabricaDoRavendb = new FabricaDoRavendb(configuraçãoDoRavendb);

            await fabricaDoRavendb.DocumentStore.Inicializar(fabricaDoRavendb.ConfiguraçãoDoRavendb, default);
        }
    }
}
