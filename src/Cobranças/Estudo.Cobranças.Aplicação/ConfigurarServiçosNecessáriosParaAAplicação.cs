using Estudo.Cobranças.Aplicação.Armazenamento.Consumidores;
using Estudo.Cobranças.Aplicação.Armazenamento.Consumidores.Configurações;
using Estudo.Cobranças.Aplicação.Armazenamento.Consumidores.Eventos;
using Estudo.Core.Infraestrutura.Armazenamento.Ravendb;
using Estudo.Core.Infraestrutura.Bus.Abstrações.Consumidor;
using Estudo.Core.Infraestrutura.Bus.Ravendb.Consumidor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Estudo.Cobranças.Aplicação
{
    public static class ConfigurarServiçosNecessáriosParaAAplicação
    {
        public static void ConfigurarServiçosDaAplicação(this IServiceCollection serviços,
            IConfiguration configuração)
        {
            var configuraçãoDaAplicaçãoDeCobranças = configuração
               .GetSection(nameof(ConfiguraçãoDaAplicaçãoDeCobranças))
               .Get<ConfiguraçãoDaAplicaçãoDeCobranças>();
            if (configuraçãoDaAplicaçãoDeCobranças?.InjetarConsumidorDoRavendb ?? false)
            {
                var configuraçãoDoRavendb = configuraçãoDaAplicaçãoDeCobranças
                    .ConfiguraçãoDoRavendbParaOConsumidorDeClientes;
                serviços.AddSingleton(new FabricaDoRavendbParaOConsumidor(
                    new FabricaDoRavendb(configuraçãoDoRavendb)));
                serviços.AddTransient<IConsumidor<Cliente>>(serviços => new ConsumidorDoRavendb<Cliente>(
                    serviços.GetRequiredService<FabricaDoRavendbParaOConsumidor>().ObterDocumentStore(),
                    configuraçãoDoRavendb));
            }
            serviços.AddTransient<ConsumidorDeClientes>();
        }
    }
}
