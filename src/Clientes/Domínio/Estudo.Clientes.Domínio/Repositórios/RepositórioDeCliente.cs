using Estudo.Clientes.Domínio.Entidades;
using Estudo.Infraestrutura.Armazenamento.Abstrações.Repositório;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Clientes.Domínio.Repositórios
{
    internal class RepositórioDeCliente : Repositório<Cliente>
    {
        private static readonly string sufixoDoId = $"-{nameof(Cliente)}";

        public override ValueTask Salvar(Cliente objeto, CancellationToken cancellationToken)
        {
            objeto.Id = CriarIdAPartirDoCpf(objeto.Cpf);
            return base.Salvar(objeto, cancellationToken);
        }

        public async ValueTask<bool> CpfCadastrado(string cpf, CancellationToken cancellationToken) =>
            (await ObterPeloId(CriarIdAPartirDoCpf(cpf), cancellationToken)) is not null;

        private static string CriarIdAPartirDoCpf(string cpf) => string.Concat(cpf, sufixoDoId);
    }
}
