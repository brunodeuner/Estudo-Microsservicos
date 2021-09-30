using Estudo.Cobranças.Domínio.Entidades;
using Estudo.Cobranças.Domínio.ManipuladoresDeEventos.Eventos;
using Estudo.Cobranças.Domínio.Repositórios;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Cobranças.Domínio.ManipuladoresDeEventos
{
    internal class ManipuladorDeEventoDePessoaCadastrada : INotificationHandler<EventoDePessoaCadastrada>
    {
        private readonly RepositórioDePessoa repositórioDePessoa;

        public ManipuladorDeEventoDePessoaCadastrada(RepositórioDePessoa repositórioDePessoa) =>
            this.repositórioDePessoa = repositórioDePessoa;

        public async Task Handle(EventoDePessoaCadastrada notification, CancellationToken cancellationToken) =>
            await repositórioDePessoa.Salvar(new Pessoa(notification.Pessoa.Cpf, notification.Pessoa.Estado),
                cancellationToken);
    }
}
