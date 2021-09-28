using Estudo.Cobranças.Aplicação.Armazenamento.Consumidores.Eventos;
using Estudo.Cobranças.Serviço.InicializaçãoDoBancoDeDados;
using Estudo.Infraestrutura.Armazenamento.Ravendb;
using Estudo.Infraestrutura.Geral;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Estudo.Cobranças.Aplicação.Testes
{
    public class TesteDeInicializaçãoDoBancoDeDados
    {
        private const string NomeDaInscrição = nameof(Cliente);

        [Fact]
        public async Task Inicializar_RavendbVálido_SubscriptionCriado()
        {
            var configuração = Configuração.CriarConfiguraçãoLendoOAppsettings();
            var configuraçãoDoRavendb = configuração
                .GetSection(nameof(ConfiguraçãoDoRavendb)).Get<ConfiguraçãoDoRavendb>();
            using var fabricaDoRavendb = new FabricaDoRavendb(configuraçãoDoRavendb);

            await fabricaDoRavendb.DocumentStore.Subscriptions.DeleteAsync(NomeDaInscrição);

            await Program.Main();

            var subscriptions = await fabricaDoRavendb.DocumentStore.Subscriptions.GetSubscriptionsAsync(
                0, 1, configuraçãoDoRavendb.Database);
            var subscription = subscriptions.Single();
            Assert.Equal(NomeDaInscrição, subscription.SubscriptionName);
        }
    }
}
