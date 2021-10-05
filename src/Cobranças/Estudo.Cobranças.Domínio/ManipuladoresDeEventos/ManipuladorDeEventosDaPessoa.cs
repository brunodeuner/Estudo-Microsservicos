using Estudo.Cobranças.Domínio.Entidades;
using Estudo.Cobranças.Domínio.Repositórios;
using Estudo.Core.Domínio.Eventos.Manutenção;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Cobranças.Domínio.ManipuladoresDeEventos
{
    internal class ManipuladorDeEventosDaPessoa :
        INotificationHandler<EventoDeEntidadeSalva<Pessoa>>,
        INotificationHandler<EventoDeEntidadeRemovida<Pessoa>>
    {
        private readonly RepositórioDePessoa repositórioDePessoa;

        public ManipuladorDeEventosDaPessoa(RepositórioDePessoa repositórioDePessoa) =>
            this.repositórioDePessoa = repositórioDePessoa;

        public async Task Handle(EventoDeEntidadeSalva<Pessoa> notification, CancellationToken cancellationToken) =>
            await repositórioDePessoa.Salvar(new Pessoa(notification.Entidade.Cpf, notification.Entidade.Estado),
                cancellationToken);

        public async Task Handle(EventoDeEntidadeRemovida<Pessoa> notification, CancellationToken cancellationToken) =>
            await repositórioDePessoa.Remover(notification.Entidade, cancellationToken);
    }
}
