using Estudo.Cobranças.Aplicação.Armazenamento.Consumidores.Eventos;
using Estudo.Infraestrutura.Bus.Abstrações.Consumidor;
using Estudo.Infraestrutura.Bus.Abstrações.Produtor;
using Estudo.Infraestrutura.Bus.Memória.Consumidor;
using Estudo.Infraestrutura.Bus.Memória.Produtor;
using Estudo.Infraestrutura.Geral;
using Estudo.Testes.Core.Http.Variáveis;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
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
                .UseConfiguration(Configuração.CriarConfiguraçãoLendoOAppsettings())
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureServices(serviceCollection =>
                {
                    var eventosPorTipo = new EventosPorTipo();
                    eventosPorTipo.AdicionarEvento("Cliente",
                        new Infraestrutura.Bus.Abstrações.Argumentos<Cliente>(new Cliente()
                        {
                            Cpf = "57251010020"
                        }));
                    serviceCollection.AddSingleton(eventosPorTipo);
                    serviceCollection.AddTransient<IProdutor, ProdutorEmMemória>();
                    serviceCollection.AddTransient<IConsumidor<Cliente>>(serviceProvider =>
                        new ConsumidorEmMemória<Cliente>(serviceProvider.GetRequiredService<EventosPorTipo>(), true));
                })
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
