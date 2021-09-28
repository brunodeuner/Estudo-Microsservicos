using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Infraestrutura.Armazenamento.Abstrações.Repositório
{
    public interface IRepositório<T> where T : class, new()
    {
        ValueTask<T> ObterPeloId(string id, CancellationToken cancellationToken);
        ValueTask Salvar(T entidade, CancellationToken cancellationToken);
    }
}
