﻿using Estudo.Infraestrutura.Geral;
using Estudo.Testes.Core.Http.Variáveis;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net.Http;

namespace Estudo.Testes.Core.Api
{
    public class WebHostFixture<TStartup> : IDisposable where TStartup : class
    {
        private TestServer servidor;

        public WebHostFixture()
        {
            var builder = new WebHostBuilder()
                .UseConfiguration(Configuração.CriarConfiguraçãoLendoOAppsettings())
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<TStartup>()
                .ConfigureServices((hostBuilder, serviços) =>
                    ConfigurarServiços(hostBuilder.Configuration, serviços));
            servidor = new TestServer(builder);

            Cliente = servidor.CreateClient();
            ServiceProvider = servidor.Services;
            Variáveis.AtribuirValorVariável("RotaBase", string.Empty);
        }

        public HttpClient Cliente { get; private set; }

        public IServiceProvider ServiceProvider { get; set; }

        public void Iniciar() => servidor.Host.Start();

        protected virtual void ConfigurarServiços(IConfiguration configuração, IServiceCollection serviços) { }

        public void Dispose()
        {
            Cliente?.Dispose();
            Cliente = default;
            servidor?.Dispose();
            servidor = default;
            GC.SuppressFinalize(this);
        }
    }
}
