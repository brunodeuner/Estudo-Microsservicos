using Estudo.Domínio.Validadores;
using FluentValidation;

namespace Estudo.Cobranças.Domínio.CasosDeUso.CobrarCliente.ManipuladoresDeComandos.Comandos.Validadores
{
    internal class ValidadorDoComandoDeCobrarCliente : ValidadorBase<ComandoDeCobrarCliente>
    {
        public ValidadorDoComandoDeCobrarCliente()
        {
            RuleFor(x => x.Cpf).NotEmpty();
            RuleFor(x => x.DataDeVencimento).NotEmpty();
            RuleFor(x => x.Valor).NotEmpty();
        }
    }
}
