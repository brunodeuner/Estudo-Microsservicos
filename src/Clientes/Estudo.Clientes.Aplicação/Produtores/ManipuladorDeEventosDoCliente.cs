using Estudo.Clientes.Domínio.Entidades;
using Estudo.Core.Domínio.Eventos.Manutenção;
using Estudo.Core.Infraestrutura.Bus.Abstrações;
using Estudo.Core.Infraestrutura.Bus.Abstrações.Produtor;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Clientes.Aplicação.Produtores
{
    internal class ManipuladorDeEventosDoCliente :
        INotificationHandler<EventoDeEntidadeSalva<Cliente>>,
        INotificationHandler<EventoDeEntidadeRemovida<Cliente>>
    {
        private readonly IProdutor produtor;

        public ManipuladorDeEventosDoCliente(IProdutor produtor) => this.produtor = produtor;

        public Task Handle(EventoDeEntidadeSalva<Cliente> notification, CancellationToken cancellationToken) =>
            produtor.EnviarAsync(nameof(EventoDeEntidadeSalva<Cliente>),
                new EventoEventArgs<EventoDeEntidadeSalva<Cliente>>(notification), cancellationToken);

        public Task Handle(EventoDeEntidadeRemovida<Cliente> notification, CancellationToken cancellationToken) =>
            produtor.EnviarAsync(nameof(EventoDeEntidadeRemovida<Cliente>),
                new EventoEventArgs<EventoDeEntidadeRemovida<Cliente>>(notification), cancellationToken);
    }
}
