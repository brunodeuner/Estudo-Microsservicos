using Estudo.Cobranças.Aplicação.Armazenamento.Consumidores.Eventos;
using Estudo.Cobranças.Serviço.Api.Configurações;
using Estudo.Infraestrutura.Bus.Abstrações.Consumidor;
using Estudo.Infraestrutura.Bus.Ravendb.Consumidor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Estudo.Cobranças.Aplicação.Armazenamento.Consumidores
{
    public static class ConfigurarServiços
    {
        public static void ConfigurarConsumidorDeClientes(this IServiceCollection serviços,
            IConfiguration configuração)
        {
            var configuraçãoDaAplicaçãoDeCobranças = configuração
               .GetSection(nameof(ConfiguraçãoDaAplicaçãoDeCobranças))
               .Get<ConfiguraçãoDaAplicaçãoDeCobranças>();
            if (configuraçãoDaAplicaçãoDeCobranças?.InjetarConsumidorDoRavendb ?? false)
                serviços.AddTransient<IConsumidor<Cliente>, ConsumidorDoRavendb<Cliente>>();

            serviços.AddTransient<ConsumidorDeClientes>();
        }
    }
}
