using Estudo.Testes.Core.Http.Variáveis;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.IO;
using System.Net.Http;

namespace Estudo.Testes.Core.Api
{
    public sealed class TestFixture<TStartup> : IDisposable where TStartup : class
    {
        private readonly TestServer servidor;

        public TestFixture()
        {
            var builder = new WebHostBuilder()
                .UseConfiguration(Configuração.Criar())
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<TStartup>();
            servidor = new TestServer(builder);
            servidor.Host.Start();
            Cliente = servidor.CreateClient();
            Variáveis.AtribuirValorVariável("RotaBase", string.Empty);
        }

        public HttpClient Cliente { get; }

        public void Dispose()
        {
            Cliente.Dispose();
            servidor.Dispose();
        }
    }
}
