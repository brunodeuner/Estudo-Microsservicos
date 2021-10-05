using Estudo.Cobranças.Domínio.Entidades;
using Estudo.Core.Domínio.Repositórios;
using Estudo.Core.Infraestrutura.Armazenamento.Abstrações;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Cobranças.Domínio.Repositórios
{
    public class RepositórioDePessoa : Repositório<Pessoa>
    {
        private static readonly string sufixoDoId = $"-{nameof(Pessoa)}";

        public RepositórioDePessoa(IDao dao, IMediator mediator) : base(dao, mediator) { }

        public override ValueTask Salvar(Pessoa objeto, CancellationToken cancellationToken)
        {
            objeto.Id = CriarIdAPartirDoCpf(objeto.Cpf);
            return base.Salvar(objeto, cancellationToken);
        }

        public async ValueTask<bool> CpfCadastrado(string cpf, CancellationToken cancellationToken) =>
            (await ObterAPartirDoCpf(cpf, cancellationToken)) is not null;

        public ValueTask<Pessoa> ObterAPartirDoCpf(string cpf, CancellationToken cancellationToken) =>
            ObterPeloId(CriarIdAPartirDoCpf(cpf), cancellationToken);

        private static string CriarIdAPartirDoCpf(string cpf) => string.Concat(cpf, sufixoDoId);

    }
}
