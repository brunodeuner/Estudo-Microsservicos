using Estudo.Cobranças.Aplicação.Armazenamento.Consumidores.Eventos;
using Estudo.Cobranças.Serviço.Api;
using Estudo.Core.Api.Testes;
using Estudo.Core.Infraestrutura.Bus.Abstrações.Consumidor;
using Estudo.Core.Infraestrutura.Bus.Abstrações.Produtor;
using Estudo.Core.Infraestrutura.Bus.Memória.Consumidor;
using Estudo.Core.Infraestrutura.Bus.Memória.Produtor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Estudo.Cobranças.Testes.DePontaAPonta
{
    public class WebHostFixtureInjetandoConsumidorEmMemória : WebHostFixture<Startup>
    {
        protected override void ConfigurarServiços(IConfiguration configuração, IServiceCollection serviços)
        {
            base.ConfigurarServiços(configuração, serviços);
            serviços.AddTransient<IProdutor, ProdutorEmMemória>();
            serviços.AddSingleton<EventosPorTipo>();
            serviços.AddSingleton<IConsumidor<Cliente>>(serviceProvider =>
                new ConsumidorEmMemória<Cliente>(serviceProvider.GetRequiredService<EventosPorTipo>(), true));
        }
    }
}
