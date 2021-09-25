﻿using Estudo.Cobranças.Domínio.Entidades;
using Raven.Client.Documents.Indexes;
using System.Linq;

namespace Estudo.Cobranças.Aplicação.Armazenamento.Ravendb.MapReduces
{
    public class CobrançasPorMêsEEstado : AbstractIndexCreationTask<
        Cobrança, Domínio.Consultas.Entidades.CobrançasPorMêsEEstado>
    {
        public CobrançasPorMêsEEstado()
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
                cobrança.Valor,
            };

        private void DefinirRedução() =>
            Reduce = cobrançasPorMêsEEstado =>
            from cobrançaPorMêsEEstado in cobrançasPorMêsEEstado
            group cobrançaPorMêsEEstado by new
            {
                cobrançaPorMêsEEstado.Mês,
            } into agrupamento
            select new
            {
                agrupamento.Key.Mês,
                Valor = agrupamento.Sum(x => x.Valor),
            };
    }
}
