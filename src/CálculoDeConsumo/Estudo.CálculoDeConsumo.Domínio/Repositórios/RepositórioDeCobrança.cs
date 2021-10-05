using Estudo.CálculoDeConsumo.Domínio.Entidades;
using Estudo.Core.Domínio.Repositórios;
using Estudo.Core.Infraestrutura.Armazenamento.Abstrações;
using MediatR;

namespace Estudo.CálculoDeConsumo.Domínio.Repositórios
{
    public class RepositórioDeCobrança : Repositório<Cobrança>
    {
        public RepositórioDeCobrança(IDao dao, IMediator mediator) : base(dao, mediator) { }
    }
}
