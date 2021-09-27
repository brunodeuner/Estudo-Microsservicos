using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace Estudo.Serviço.Api
{
    public static class CriarHostBuilder
    {
        public static void CriarERodar<TStartup>() where TStartup : class =>
            CreateHostBuilder<TStartup>().Build().Run();

        private static IHostBuilder CreateHostBuilder<TStartup>() where TStartup : class =>
            Host.CreateDefaultBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<TStartup>());
    }
}
