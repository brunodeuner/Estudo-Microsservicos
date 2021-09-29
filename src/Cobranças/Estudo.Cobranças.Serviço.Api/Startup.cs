using Estudo.Cobranças.Aplicação;
using Estudo.Cobranças.Serviço.Api.Consumidores;
using Estudo.Core.Serviço.Api;
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
            serviços.ConfigurarServiçoEAplicação(configuração);
            serviços.ConfigurarServiçosDaAplicação(configuração);
            serviços.AddHostedService<ServiçoDeConsumidorDeClientes>();
        }

        public static void Configure(IApplicationBuilder aplicação) => aplicação.Configurar();
    }
}
