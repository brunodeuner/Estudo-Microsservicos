using Estudo.Domínio.Comandos;
using System;

namespace Estudo.Cobranças.Domínio.CasosDeUso.CobrarCliente.ManipuladoresDeComandos.Comandos
{
    public class ComandoDeCobrarCliente : ComandoSemRetorno
    {
        public string Cpf { get; init; }
        public DateTime DataDeVencimento { get; init; }
        public decimal Valor { get; init; }
    }
}
