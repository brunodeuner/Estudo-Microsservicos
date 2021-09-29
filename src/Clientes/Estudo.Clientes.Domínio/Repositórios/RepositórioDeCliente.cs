using Estudo.Clientes.Domínio.Entidades;
using Estudo.Core.Infraestrutura.Armazenamento.Abstrações;
using Estudo.Core.Infraestrutura.Armazenamento.Abstrações.Repositório;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Clientes.Domínio.Repositórios
{
    public class RepositórioDeCliente : Repositório<Cliente>
    {
        private static readonly string sufixoDoId = $"-{nameof(Cliente)}";

        public RepositórioDeCliente(IDao dao) : base(dao) { }

        public override ValueTask Salvar(Cliente objeto, CancellationToken cancellationToken)
        {
            objeto.Id = CriarIdAPartirDoCpf(objeto.Cpf);
            return base.Salvar(objeto, cancellationToken);
        }

        public ValueTask<Cliente> ObterAPartirDoCpf(string cpf, CancellationToken cancellationToken) =>
            ObterPeloId(CriarIdAPartirDoCpf(cpf), cancellationToken);

        public async ValueTask<bool> CpfNãoCadastrado(string cpf, CancellationToken cancellationToken) =>
            (await ObterAPartirDoCpf(cpf, cancellationToken)) is null;

        private static string CriarIdAPartirDoCpf(string cpf) => string.Concat(cpf, sufixoDoId);
    }
}
