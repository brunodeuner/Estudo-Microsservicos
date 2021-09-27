using Estudo.Serviço.Api;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Estudo.CálculoDeConsumo.Serviço.Api
{
    public class Startup
    {
        private readonly IConfiguration configuração;

        public Startup(IConfiguration configuração) => this.configuração = configuração;

        public void ConfigureServices(IServiceCollection serviços) => serviços.ConfigurarServiçoEAplicação(configuração);

        public static void Configure(IApplicationBuilder aplicação) => aplicação.Configurar();
    }
}
