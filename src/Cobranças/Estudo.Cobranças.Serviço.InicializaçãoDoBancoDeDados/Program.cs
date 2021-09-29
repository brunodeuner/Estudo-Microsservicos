using Estudo.Cobranças.Aplicação.Armazenamento.Consumidores.Configurações;
using Estudo.Cobranças.Aplicação.Armazenamento.Ravendb.MapReduces;
using Estudo.Cobranças.Aplicação.Armazenamento.Ravendb.Subscriptions;
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
            await InicializarMapReduces();
            await InicializarSubscriptions();
        }

        private static async Task InicializarMapReduces()
        {
            var configuração = Configuração.CriarConfiguraçãoLendoOAppsettings();
            var configuraçãoDoRavendb = configuração.GetSection(nameof(ConfiguraçãoDoRavendb))
                .Get<ConfiguraçãoDoRavendb>();
            using var fabricaDoRavendb = new FabricaDoRavendb(configuraçãoDoRavendb);

            await fabricaDoRavendb.DocumentStore.CriarMapReduces(default);
        }

        private static async Task InicializarSubscriptions()
        {
            var configuração = Configuração.CriarConfiguraçãoLendoOAppsettings();
            var configuraçãoDaAplicaçãoDeCobranças = configuração
               .GetSection(nameof(ConfiguraçãoDaAplicaçãoDeCobranças))
               .Get<ConfiguraçãoDaAplicaçãoDeCobranças>();

            using var fabricaDoRavendb = new FabricaDoRavendb(configuraçãoDaAplicaçãoDeCobranças
                .ConfiguraçãoDoRavendbParaOConsumidorDeClientes);

            await fabricaDoRavendb.DocumentStore.CriarSubscriptions(configuraçãoDaAplicaçãoDeCobranças
                .ConfiguraçãoDoRavendbParaOConsumidorDeClientes, default);
        }
    }
}
