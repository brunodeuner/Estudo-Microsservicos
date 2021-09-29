using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Estudo.Core.Infraestrutura.Armazenamento.Abstrações.Queryable.Leitura
{
    public interface IToAsyncEnumerable
    {
        IAsyncEnumerable<T> ToAsyncEnumerable<T>(IQueryable<T> query, CancellationToken cancellationToken)
            where T : class, new();
    }
}
