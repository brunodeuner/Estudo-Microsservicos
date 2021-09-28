using Estudo.Cobranças.Domínio.Consultas.Entidades;
using Estudo.Infraestrutura.Armazenamento.Abstrações;
using Estudo.Infraestrutura.Armazenamento.Abstrações.Repositório;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Estudo.Cobranças.Domínio.Consultas.Repositórios
{
    public class RepositórioDeCobrançasPorMêsEEstado : Repositório<CobrançasPorMêsEEstado>
    {
        public RepositórioDeCobrançasPorMêsEEstado(IDao dao) : base(dao) { }

        public IAsyncEnumerable<CobrançasPorMêsEEstado> ObterTodas(CancellationToken cancellationToken) =>
            Selecionar().ToAsyncEnumerable(cancellationToken);
    }
}
