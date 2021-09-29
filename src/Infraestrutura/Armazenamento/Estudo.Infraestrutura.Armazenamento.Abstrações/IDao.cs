using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Core.Infraestrutura.Armazenamento.Abstrações
{
    public interface IDao
    {
        IQueryable<T> Selecionar<T>();
        ValueTask<T> ObterPeloId<T>(string id, CancellationToken cancellationToken);
        ValueTask Salvar<T>(T objeto, CancellationToken cancellationToken) where T : class, new();
        ValueTask SalvarAlterações(CancellationToken cancellationToken);
    }
}
