using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Serviço.Api
{
    public static class CriarHostBuilder
    {
        public static Task CriarERodar<TStartup>(string[] args = default,
            CancellationToken cancellationToken = default) where TStartup : class =>
            CreateHostBuilder<TStartup>(args).Build().RunAsync(cancellationToken);

        private static IHostBuilder CreateHostBuilder<TStartup>(string[] args) where TStartup : class =>
            Host.CreateDefaultBuilder(args)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<TStartup>());
    }
}
