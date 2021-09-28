using Estudo.Cobranças.Domínio.Entidades;
using Raven.Client.Documents.Indexes;
using System.Linq;

namespace Estudo.Cobranças.Aplicação.Armazenamento.Ravendb.MapReduces
{
    public class MapReduceDeCobrançasPorMêsEEstado : AbstractIndexCreationTask<
        Cobrança, Domínio.Consultas.Entidades.CobrançasPorMêsEEstado>
    {
        public MapReduceDeCobrançasPorMêsEEstado()
        {
            DefinirMapeamento();
            DefinirRedução();
            OutputReduceToCollection = nameof(Domínio.Consultas.Entidades.CobrançasPorMêsEEstado);
        }

        private void DefinirMapeamento() =>
            Map = cobranças =>
            from cobrança in cobranças
            select new
            {
                Mês = cobrança.DataDeVencimento.Month,
                cobrança.Pessoa.Estado,
                cobrança.Valor,
            };

        private void DefinirRedução() =>
            Reduce = cobrançasPorMêsEEstado =>
            from cobrançaPorMêsEEstado in cobrançasPorMêsEEstado
            group cobrançaPorMêsEEstado by new
            {
                cobrançaPorMêsEEstado.Mês,
                cobrançaPorMêsEEstado.Estado,
            } into agrupamento
            select new
            {
                agrupamento.Key.Mês,
                agrupamento.Key.Estado,
                Valor = agrupamento.Sum(x => x.Valor),
            };
    }
}
