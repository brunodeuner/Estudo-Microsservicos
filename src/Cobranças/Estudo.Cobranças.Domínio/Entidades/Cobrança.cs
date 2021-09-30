using Estudo.Core.Domínio.Entidades;
using System;

namespace Estudo.Cobranças.Domínio.Entidades
{
    public class Cobrança : Entidade
    {
        public Cobrança() { }

        public Cobrança(Pessoa pessoa, DateTime dataDeVencimento, decimal valor)
        {
            Pessoa = pessoa;
            DataDeVencimento = dataDeVencimento;
            Valor = valor;
        }

        public Pessoa Pessoa { get; init; }
        public DateTime DataDeVencimento { get; init; }
        public decimal Valor { get; init; }
    }
}
