using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Core.Infraestrutura.Armazenamento.Abstrações.Repositório
{
    public interface IRepositório<T> where T : class, new()
    {
        ValueTask<T> ObterPeloId(string id, CancellationToken cancellationToken);
        ValueTask Salvar(T entidade, CancellationToken cancellationToken);
        ValueTask Remover(T entidade, CancellationToken cancellationToken);
    }
}
