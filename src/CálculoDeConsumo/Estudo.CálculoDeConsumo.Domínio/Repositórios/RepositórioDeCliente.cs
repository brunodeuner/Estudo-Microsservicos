using Estudo.CálculoDeConsumo.Domínio.Entidades;
using Estudo.Infraestrutura.Armazenamento.Abstrações;
using Estudo.Infraestrutura.Armazenamento.Abstrações.Repositório;
using System.Collections.Generic;
using System.Linq;
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
