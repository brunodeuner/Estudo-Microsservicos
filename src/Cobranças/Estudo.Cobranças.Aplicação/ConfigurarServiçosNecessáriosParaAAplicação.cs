using Estudo.Cobranças.Aplicação.Armazenamento.Consumidores;
using Estudo.Cobranças.Aplicação.Armazenamento.Consumidores.Configurações;
using Estudo.Cobranças.Aplicação.Armazenamento.Consumidores.Eventos;
using Estudo.Core.Domínio.Eventos.Manutenção;
using Estudo.Core.Infraestrutura.Bus.Abstrações.Consumidor;
using Estudo.Core.Infraestrutura.Bus.RabbitMq.Consumidor;
using Estudo.Core.Infraestrutura.Geral.Json.Abstrações;
using Estudo.Core.Infraestrutura.Geral.Json.SystemTextJson;
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
            if (configuraçãoDaAplicaçãoDeCobranças?.InjetarConsumidorDoRabbitMq ?? false)
            {
                serviços.AddSingleton(configuraçãoDaAplicaçãoDeCobranças.ConfiguraçãoDaFila);
                serviços.AddTransient<IDeserializador, Deserializador>();
                serviços.AddTransient<IConsumidor<EventoDeEntidadeRemovida<Cliente>>,
                    ConsumidorDoRabbitMq<EventoDeEntidadeRemovida<Cliente>>>();
                serviços.AddTransient<IConsumidor<EventoDeEntidadeSalva<Cliente>>,
                    ConsumidorDoRabbitMq<EventoDeEntidadeSalva<Cliente>>>();
            }
            serviços.AddTransient<ConsumidorDeClientes>();
        }
    }
}
