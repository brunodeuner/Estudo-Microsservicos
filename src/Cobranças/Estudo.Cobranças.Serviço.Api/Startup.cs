using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Estudo.Cobranças.Serviço.Api
{
    internal class Startup
    {
        private readonly IConfiguration configuração;

        public Startup(IConfiguration configuração) => this.configuração = configuração;

        public static void ConfigureServices(IServiceCollection serviços) => serviços.AddControllers();

        public static void Configure(IApplicationBuilder aplicação)
        {
            aplicação.UseHttpsRedirection();
            aplicação.UseRouting();
            aplicação.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
