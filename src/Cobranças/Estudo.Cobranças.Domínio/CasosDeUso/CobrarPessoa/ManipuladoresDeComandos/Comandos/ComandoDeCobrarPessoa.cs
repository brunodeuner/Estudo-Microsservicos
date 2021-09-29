using Estudo.Cobranças.Domínio.CasosDeUso.CobrarPessoa.ManipuladoresDeComandos.Comandos.Validadores;
using Estudo.Core.Domínio.Comandos;
using Estudo.Core.Domínio.Validadores;
using System;

namespace Estudo.Cobranças.Domínio.CasosDeUso.CobrarPessoa.ManipuladoresDeComandos.Comandos
{
    [Validador(typeof(ValidadorDoComandoDeCobrarPessoa))]
    [Validador(typeof(ValidadorDeCpfCadastrado))]
    public class ComandoDeCobrarPessoa : ComandoSemRetorno
    {
        public string Cpf { get; init; }
        public DateTime DataDeVencimento { get; init; }
        public decimal Valor { get; init; }
    }
}
