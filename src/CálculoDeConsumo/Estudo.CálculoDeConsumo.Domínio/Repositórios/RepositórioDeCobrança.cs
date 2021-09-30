using Estudo.CálculoDeConsumo.Domínio.Entidades;
using Estudo.Core.Infraestrutura.Armazenamento.Abstrações;
using Estudo.Core.Infraestrutura.Armazenamento.Abstrações.Repositório;

namespace Estudo.CálculoDeConsumo.Domínio.Repositórios
{
    public class RepositórioDeCobrança : Repositório<Cobrança>
    {
        public RepositórioDeCobrança(IDao dao) : base(dao) { }
    }
}
