using Estudo.Cobranças.Aplicação.Armazenamento.Consumidores;
using Estudo.Cobranças.Aplicação.Armazenamento.Consumidores.Eventos;
using Estudo.Cobranças.Serviço.Api.Configurações;
using Estudo.Cobranças.Serviço.Api.Consumidores;
using Estudo.Infraestrutura.Bus.Abstrações.Consumidor;
using Estudo.Infraestrutura.Bus.Ravendb.Consumidor;
using Estudo.Serviço.Api;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Estudo.Cobranças.Serviço.Api
{
    public class Startup
    {
        private readonly IConfiguration configuração;

        public Startup(IConfiguration configuração) => this.configuração = configuração;

        public void ConfigureServices(IServiceCollection serviços)
        {
            var configuraçãoDaAplicaçãoDeCobranças = configuração
                .GetSection(nameof(ConfiguraçãoDaAplicaçãoDeCobranças))
                .Get<ConfiguraçãoDaAplicaçãoDeCobranças>();
            if (configuraçãoDaAplicaçãoDeCobranças?.InjetarConsumidorDoRavendb ?? false)
                serviços.AddTransient<IConsumidor<Cliente>, ConsumidorDoRavendb<Cliente>>();

            serviços.AddTransient<ConsumidorDeClientes>();
            serviços.ConfigurarServiçoEAplicação(configuração);
            serviços.AddHostedService<ServiçoDeConsumidorDeClientes>();
        }

        public static void Configure(IApplicationBuilder aplicação) => aplicação.Configurar();
    }
}
