using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Threading.Tasks;

namespace Estudo.Cobranças.Serviço.Api
{
    public static class Program
    {
        public static async Task Main() => await CreateHostBuilder().Build().RunAsync(default);

        private static IHostBuilder CreateHostBuilder() => Host.CreateDefaultBuilder()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}
