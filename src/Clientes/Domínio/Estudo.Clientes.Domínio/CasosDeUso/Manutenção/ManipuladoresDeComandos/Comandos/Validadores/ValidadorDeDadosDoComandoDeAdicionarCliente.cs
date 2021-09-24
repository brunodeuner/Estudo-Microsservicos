using Estudo.Clientes.Domínio.Especificações;
using Estudo.Clientes.Domínio.Repositórios;
using Estudo.Domínio.Validadores;
using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;

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

    internal class ValidadorDeCpfDuplicadoDoComandoDeAdicionarCliente : ValidadorBase<ComandoDeAdicionarCliente>
    {
        public ValidadorDeCpfDuplicadoDoComandoDeAdicionarCliente(RepositórioDeCliente repositórioDeCliente) =>
            RuleFor(x => x.Cpf)
                .MustAsync(CpfJáCadastrado(repositórioDeCliente))
                .WithMessage(ValidaçãoDeCpf.MensagemDeCpfJáCadastrado);

        private static Func<string, CancellationToken, Task<bool>> CpfJáCadastrado(
            RepositórioDeCliente repositórioDeCliente) => async (string cpf, CancellationToken cancellationToken) =>
                await repositórioDeCliente.CpfCadastrado(cpf, cancellationToken);
    }
}
