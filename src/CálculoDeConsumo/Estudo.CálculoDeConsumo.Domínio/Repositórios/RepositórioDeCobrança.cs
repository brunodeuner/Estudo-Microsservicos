using Estudo.CálculoDeConsumo.Domínio.Entidades;
using Estudo.Infraestrutura.Armazenamento.Abstrações;
using Estudo.Infraestrutura.Armazenamento.Abstrações.Repositório;

namespace Estudo.CálculoDeConsumo.Domínio.Repositórios
{
    public class RepositórioDeCobrança : Repositório<Cobrança>
    {
        public RepositórioDeCobrança(IDao dao) : base(dao) { }
    }
}
