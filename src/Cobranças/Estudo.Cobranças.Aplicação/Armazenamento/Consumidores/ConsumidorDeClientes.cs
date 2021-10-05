using Estudo.Cobranças.Aplicação.Armazenamento.Consumidores.Eventos;
using Estudo.Core.Domínio.Eventos.Manutenção;
using Estudo.Core.Domínio.Validação;
using Estudo.Core.Infraestrutura.Armazenamento.Abstrações;
using Estudo.Core.Infraestrutura.Bus.Abstrações;
using Estudo.Core.Infraestrutura.Bus.Abstrações.Consumidor;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Cobranças.Aplicação.Armazenamento.Consumidores
{
    public class ConsumidorDeClientes
    {
        private readonly IConsumidor<EventoDeEntidadeRemovida<Cliente>> consumidorDeClienteRemovido;
        private readonly IConsumidor<EventoDeEntidadeSalva<Cliente>> consumidorDeClienteSalvo;
        private readonly IServiceProvider serviceProvider;

        public ConsumidorDeClientes(IConsumidor<EventoDeEntidadeRemovida<Cliente>> consumidorDeClienteRemovido,
            IConsumidor<EventoDeEntidadeSalva<Cliente>> consumidorDeClienteSalvo, IServiceProvider serviceProvider)
        {
            this.consumidorDeClienteRemovido = consumidorDeClienteRemovido;
            this.consumidorDeClienteSalvo = consumidorDeClienteSalvo;
            this.serviceProvider = serviceProvider;
        }

        public async Task Iniciar(CancellationToken cancellationToken)
        {
            consumidorDeClienteRemovido.Consumir +=
                (requisição, cancellationToken) => PublicarEvento(requisição, cancellationToken);
            consumidorDeClienteSalvo.Consumir +=
                (requisição, cancellationToken) => PublicarEvento(requisição, cancellationToken);
            await Task.WhenAll(
                consumidorDeClienteRemovido.Iniciar(nameof(EventoDeEntidadeRemovida<Cliente>), cancellationToken),
                consumidorDeClienteSalvo.Iniciar(nameof(EventoDeEntidadeSalva<Cliente>), cancellationToken));
        }

        private async Task PublicarEvento<T>(EventoEventArgs<T> requisição, CancellationToken cancellationToken)
        {
            using var escopo = serviceProvider.CreateScope();
            var serviceProviderDoEscopo = escopo.ServiceProvider;
            await serviceProviderDoEscopo.GetRequiredService<IMediator>().Publish(requisição.Corpo, cancellationToken);
            if (PossuiNotificações(serviceProviderDoEscopo))
                return;
            await SalvarAlterações(serviceProviderDoEscopo, cancellationToken);
        }

        private static bool PossuiNotificações(IServiceProvider serviços) =>
            serviços.GetRequiredService<INotificaçõesDoDomínio>().PossuiNotificações();

        private static ValueTask SalvarAlterações(IServiceProvider serviços, CancellationToken cancellationToken) =>
            serviços.GetRequiredService<IDao>().SalvarAlterações(cancellationToken);
    }
}
