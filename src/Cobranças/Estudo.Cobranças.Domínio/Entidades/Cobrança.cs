using Estudo.Domínio.Entidades;
using System;

namespace Estudo.Cobranças.Domínio.Entidades
{
    public class Cobrança : Entidade
    {
        public string Cpf { get; init; }
        public DateTime DataDeVencimento { get; init; }
        public decimal Valor { get; init; }
    }
}
