using Estudo.Domínio.Validadores;
using FluentValidation;

namespace Estudo.Cobranças.Domínio.CasosDeUso.CobrarPessoa.ManipuladoresDeComandos.Comandos.Validadores
{
    internal class ValidadorDoComandoDeCobrarPessoa : ValidadorBase<ComandoDeCobrarPessoa>
    {
        public ValidadorDoComandoDeCobrarPessoa()
        {
            RuleFor(x => x.Cpf).NotEmpty();
            RuleFor(x => x.DataDeVencimento).NotEmpty();
            RuleFor(x => x.Valor).GreaterThan(0);
        }
    }
}
