using Estudo.CálculoDeConsumo.Domínio.Entidades;
using Estudo.Core.Domínio.Repositórios;
using Estudo.Core.Infraestrutura.Armazenamento.Abstrações;
using Estudo.Core.Infraestrutura.Armazenamento.Abstrações.Queryable;
using MediatR;
using System.Collections.Generic;
using System.Threading;

namespace Estudo.CálculoDeConsumo.Domínio.Repositórios
{
    public class RepositórioDeCliente : Repositório<Cliente>
    {
        public RepositórioDeCliente(IDao dao, IMediator mediator) : base(dao, mediator) { }

        public IAsyncEnumerable<Cliente> ObterTodos(CancellationToken cancellationToken) =>
            Selecionar().ToAsyncEnumerable(cancellationToken);
    }
}
