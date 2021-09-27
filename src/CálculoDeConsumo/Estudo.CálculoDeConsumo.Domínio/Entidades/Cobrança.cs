using Estudo.Domínio.Entidades;
using System;

namespace Estudo.CálculoDeConsumo.Domínio.Entidades
{
    public class Cobrança : Entidade
    {
        public Cobrança() { }

        public Cobrança(string cpf, DateTime dataDeVencimento)
        {
            Cpf = cpf;
            DataDeVencimento = dataDeVencimento;
            Valor = int.Parse($"{Cpf.Substring(0, 2)}{Cpf[^2..]}");
        }

        public string Cpf { get; init; }
        public DateTime DataDeVencimento { get; init; }
        public int Valor { get; init; }
    }
}
