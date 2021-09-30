using Estudo.Clientes.Domínio.Especificações;
using Estudo.Core.Domínio.Validadores;
using FluentValidation;

namespace Estudo.Clientes.Domínio.CasosDeUso.Manutenção.ManipuladoresDeComandos.Comandos.Validadores
{
    internal class ValidadorDeDadosDoComandoDeAdicionarCliente : ValidadorBase<ComandoDeAdicionarCliente>
    {
        public ValidadorDeDadosDoComandoDeAdicionarCliente()
        {
            RuleFor(x => x.Nome).NotEmpty();
            RuleFor(x => x.Estado).NotEmpty();
            RuleFor(x => x.Cpf)
                .Must(ValidaçãoDeCpf.ÉValido)
                .WithMessage(ValidaçãoDeCpf.MensagemDeCpfInválido);
        }
    }
}
