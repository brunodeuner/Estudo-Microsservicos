using Estudo.Cobranças.Domínio.Entidades;
using Estudo.Cobranças.Serviço.Api;
using Estudo.Core.Api.Testes;
using Estudo.Core.Domínio.Eventos.Manutenção;
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
            serviços.AddSingleton<IConsumidor<EventoDeEntidadeRemovida<Pessoa>>>(serviceProvider =>
                new ConsumidorEmMemória<EventoDeEntidadeRemovida<Pessoa>>(
                    serviceProvider.GetRequiredService<EventosPorTipo>(), true));
            serviços.AddSingleton<IConsumidor<EventoDeEntidadeSalva<Pessoa>>>(serviceProvider =>
                new ConsumidorEmMemória<EventoDeEntidadeSalva<Pessoa>>(
                    serviceProvider.GetRequiredService<EventosPorTipo>(), true));
        }
    }
}
