using Estudo.Cobranças.Aplicação.Armazenamento.Consumidores;
using Estudo.Cobranças.Serviço.Api.Consumidores;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Threading.Tasks;

namespace Estudo.Cobranças.Serviço.Api
{
    public static class Program
    {
        public static async Task Main() => await CreateHostBuilder<Startup>().Build().RunAsync(default);

        public static void ConfigurarConsumidorDeClientes(IConfiguration configuração, IServiceCollection serviços)
        {
            serviços.ConfigurarConsumidorDeClientes(configuração);
            serviços.AddHostedService<ServiçoDeConsumidorDeClientes>();
        }

        private static IHostBuilder CreateHostBuilder<TStartup>() where TStartup : class =>
            Host.CreateDefaultBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
                .ConfigureServices((hostBuilder, serviços) =>
                    ConfigurarConsumidorDeClientes(hostBuilder.Configuration, serviços));
    }
}
