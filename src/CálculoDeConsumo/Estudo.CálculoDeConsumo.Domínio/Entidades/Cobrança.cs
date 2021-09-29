using Estudo.Domínio.Entidades;
using System;

namespace Estudo.CálculoDeConsumo.Domínio.Entidades
{
    public class Cobrança : Entidade
    {
        public Cobrança() { }

        private Cobrança(string cpf, DateTime dataDeVencimento, int valor)
        {
            Cpf = cpf;
            DataDeVencimento = dataDeVencimento;
            Valor = valor;
        }

        public static Cobrança CriarComValorDeCobrançaAPartirDoCpf(string cpf, DateTime dataDeVencimento) =>
            new(cpf, dataDeVencimento, int.Parse($"{cpf.Substring(0, 2)}{cpf[^2..]}"));

        public string Cpf { get; init; }
        public DateTime DataDeVencimento { get; init; }
        public int Valor { get; init; }
    }
}
