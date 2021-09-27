using Estudo.Cobranças.Domínio.CasosDeUso.CobrarCliente.ManipuladoresDeComandos.Comandos.Validadores;
using Estudo.Domínio.Comandos;
using Estudo.Domínio.Validadores;
using System;

namespace Estudo.Cobranças.Domínio.CasosDeUso.CobrarCliente.ManipuladoresDeComandos.Comandos
{
    [Validador(typeof(ValidadorDoComandoDeCobrarCliente))]
    [Validador(typeof(ValidadorDeCpfCadastrado))]
    public class ComandoDeCobrarCliente : ComandoSemRetorno
    {
        public string Cpf { get; init; }
        public DateTime DataDeVencimento { get; init; }
        public decimal Valor { get; init; }
    }
}
