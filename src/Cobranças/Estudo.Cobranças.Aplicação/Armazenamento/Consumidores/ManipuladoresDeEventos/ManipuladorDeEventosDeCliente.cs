using Estudo.Cobranças.Aplicação.Armazenamento.Consumidores.Eventos;
using Estudo.Cobranças.Domínio.Repositórios;
using Estudo.Core.Domínio.Eventos.Manutenção;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Cobranças.Aplicação.Armazenamento.Consumidores.ManipuladoresDeEventos
{
    internal class ManipuladorDeEventosDeCliente :
        INotificationHandler<EventoDeEntidadeSalva<Cliente>>,
        INotificationHandler<EventoDeEntidadeRemovida<Cliente>>
    {
        private readonly RepositórioDePessoa repositórioDePessoa;

        public ManipuladorDeEventosDeCliente(RepositórioDePessoa repositórioDePessoa) =>
            this.repositórioDePessoa = repositórioDePessoa;

        public async Task Handle(EventoDeEntidadeSalva<Cliente> notification, CancellationToken cancellationToken) =>
            await repositórioDePessoa.Salvar(notification.Entidade, cancellationToken);

        public async Task Handle(EventoDeEntidadeRemovida<Cliente> notification, CancellationToken cancellationToken) =>
            await repositórioDePessoa.Remover(notification.Entidade, cancellationToken);
    }
}
