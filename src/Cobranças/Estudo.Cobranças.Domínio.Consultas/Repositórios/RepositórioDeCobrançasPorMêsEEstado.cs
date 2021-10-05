using Estudo.Cobranças.Domínio.Consultas.Entidades;
using Estudo.Core.Domínio.Repositórios;
using Estudo.Core.Infraestrutura.Armazenamento.Abstrações;
using Estudo.Core.Infraestrutura.Armazenamento.Abstrações.Queryable;
using MediatR;
using System.Collections.Generic;
using System.Threading;

namespace Estudo.Cobranças.Domínio.Consultas.Repositórios
{
    public class RepositórioDeCobrançasPorMêsEEstado : Repositório<CobrançasPorMêsEEstado>
    {
        public RepositórioDeCobrançasPorMêsEEstado(IDao dao, IMediator mediator) : base(dao, mediator) { }

        public IAsyncEnumerable<CobrançasPorMêsEEstado> ObterTodas(CancellationToken cancellationToken) =>
            Selecionar().ToAsyncEnumerable(cancellationToken);
    }
}
