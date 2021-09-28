using Estudo.Cobranças.Aplicação.Armazenamento.Consumidores.Eventos;
using Estudo.Cobranças.Serviço.Api;
using Estudo.Infraestrutura.Bus.Abstrações.Consumidor;
using Estudo.Infraestrutura.Bus.Abstrações.Produtor;
using Estudo.Infraestrutura.Bus.Memória.Consumidor;
using Estudo.Infraestrutura.Bus.Memória.Produtor;
using Estudo.Testes.Core.Api;
using Microsoft.Extensions.DependencyInjection;

namespace Estudo.Testes.Cobranças.DePontaAPonta
{
    public class WebHostFixtureInjetandoConsumidorEmMemória : WebHostFixture<Startup>
    {
        protected override void ConfigurarServiços(IServiceCollection serviceCollection)
        {
            base.ConfigurarServiços(serviceCollection);
            serviceCollection.AddTransient<IProdutor, ProdutorEmMemória>();
            serviceCollection.AddSingleton<EventosPorTipo>();
            serviceCollection.AddSingleton<IConsumidor<Cliente>>(serviceProvider =>
                new ConsumidorEmMemória<Cliente>(serviceProvider.GetRequiredService<EventosPorTipo>(), true));
        }
    }
}
