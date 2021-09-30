using Estudo.Cobranças.Aplicação.Armazenamento.Consumidores.Eventos;
using Estudo.Cobranças.Domínio.ManipuladoresDeEventos.Eventos;
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
        private readonly IConsumidor<Cliente> consumidor;
        private readonly IServiceProvider serviceProvider;

        public ConsumidorDeClientes(IConsumidor<Cliente> consumidor, IServiceProvider serviceProvider)
        {
            this.consumidor = consumidor;
            this.serviceProvider = serviceProvider;
        }

        public Task Iniciar(CancellationToken cancellationToken)
        {
            consumidor.Consumir += async (requisição, cancellationToken) =>
            {
                using var escopo = serviceProvider.CreateScope();
                var serviceProviderDoEscopo = escopo.ServiceProvider;
                await PublicarEventoDePessoadaCadastrada(requisição, serviceProviderDoEscopo, cancellationToken);
                if (PossuiNotificações(serviceProviderDoEscopo))
                    return;
                await SalvarAlterações(serviceProviderDoEscopo, cancellationToken);
            };
            return consumidor.Iniciar(nameof(Cliente), cancellationToken);
        }

        private static Task PublicarEventoDePessoadaCadastrada(EventoEventArgs<Cliente> requisição,
            IServiceProvider serviços, CancellationToken cancellationToken) => serviços.GetRequiredService<IMediator>()
            .Publish(new EventoDePessoaCadastrada(requisição.Corpo), cancellationToken);

        private static bool PossuiNotificações(IServiceProvider serviços) =>
            serviços.GetRequiredService<INotificaçõesDoDomínio>().PossuiNotificações();

        private static ValueTask SalvarAlterações(IServiceProvider serviços, CancellationToken cancellationToken) =>
            serviços.GetRequiredService<IDao>().SalvarAlterações(cancellationToken);
    }
}
