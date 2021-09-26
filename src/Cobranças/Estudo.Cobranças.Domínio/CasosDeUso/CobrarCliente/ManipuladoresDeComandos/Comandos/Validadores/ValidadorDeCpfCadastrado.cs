using Estudo.Cobranças.Domínio.Entidades;
using Estudo.Cobranças.Domínio.Repositórios;
using Estudo.Domínio.Validadores;
using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Cobranças.Domínio.CasosDeUso.CobrarCliente.ManipuladoresDeComandos.Comandos.Validadores
{
    internal class ValidadorDeCpfCadastrado : ValidadorBase<ComandoDeCobrarCliente>
    {
        public ValidadorDeCpfCadastrado(RepositórioDePessoa repositórioDePessoa) =>
            RuleFor(x => x.Cpf)
                .MustAsync(CpfCadastrado(repositórioDePessoa))
                .WithMessage($"{nameof(Pessoa.Cpf)} não cadastrado");

        private static Func<string, CancellationToken, Task<bool>> CpfCadastrado(
            RepositórioDePessoa repositórioDePessoa) => async (string cpf, CancellationToken cancellationToken) =>
                await repositórioDePessoa.CpfCadastrado(cpf, cancellationToken);
    }
}
