using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Infraestrutura.Armazenamento.Abstrações.Repositório
{
    public interface IRepositório<T> where T : class, new()
    {
        Task<T> ObterPeloId(string id, CancellationToken cancellationToken);
        Task Salvar(T entidade, CancellationToken cancellationToken);
        Task Remover(T objeto, CancellationToken cancellationToken);
    }
}
