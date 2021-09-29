using Estudo.CálculoDeConsumo.Domínio.Entidades;
using Estudo.Core.Infraestrutura.Armazenamento.Abstrações;
using Estudo.Core.Infraestrutura.Armazenamento.Abstrações.Queryable;
using Estudo.Core.Infraestrutura.Armazenamento.Abstrações.Repositório;
using System.Collections.Generic;
using System.Threading;

namespace Estudo.CálculoDeConsumo.Domínio.Repositórios
{
    public class RepositórioDeCliente : Repositório<Cliente>
    {
        public RepositórioDeCliente(IDao dao) : base(dao) { }

        public IAsyncEnumerable<Cliente> ObterTodos(CancellationToken cancellationToken) =>
            Selecionar().ToAsyncEnumerable(cancellationToken);
    }
}
