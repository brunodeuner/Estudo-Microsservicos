using Estudo.Clientes.Domínio.Entidades;
using Estudo.Core.Domínio.Repositórios;
using Estudo.Core.Infraestrutura.Armazenamento.Abstrações;
using Estudo.Core.Infraestrutura.Armazenamento.Abstrações.Queryable;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Clientes.Domínio.Repositórios
{
    public class RepositórioDeCliente : Repositório<Cliente>
    {
        private static readonly string sufixoDoId = $"-{nameof(Cliente)}";

        public RepositórioDeCliente(IDao dao, IMediator mediator) : base(dao, mediator) { }

        public override ValueTask Salvar(Cliente objeto, CancellationToken cancellationToken)
        {
            objeto.Id = CriarIdAPartirDoCpf(objeto.Cpf);
            return base.Salvar(objeto, cancellationToken);
        }

        public IAsyncEnumerable<Cliente> ObterTodos(CancellationToken cancellationToken) =>
            Selecionar().ToAsyncEnumerable(cancellationToken);

        public ValueTask<Cliente> ObterAPartirDoCpf(string cpf, CancellationToken cancellationToken) =>
            ObterPeloId(CriarIdAPartirDoCpf(cpf), cancellationToken);

        public async ValueTask<bool> CpfNãoCadastrado(string cpf, CancellationToken cancellationToken) =>
            (await ObterAPartirDoCpf(cpf, cancellationToken)) is null;

        private static string CriarIdAPartirDoCpf(string cpf) => string.Concat(cpf, sufixoDoId);
    }
}
