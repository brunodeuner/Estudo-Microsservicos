using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Infraestrutura.Armazenamento.Abstrações
{
    public interface IDao
    {
        IQueryable<T> Selecionar<T>();
        ValueTask<T> ObterPeloId<T>(string id, CancellationToken cancellationToken);
        Task Adicionar<T>(T objeto, CancellationToken cancellationToken) where T : class, new();
        ValueTask Atualizar<T>(T objeto, CancellationToken cancellationToken) where T : class, new();
        ValueTask Remover<T>(T objeto, CancellationToken cancellationToken) where T : class, new();
        ValueTask SalvarAlterações(CancellationToken cancellationToken);
    }
}
