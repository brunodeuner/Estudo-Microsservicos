using Estudo.Infraestrutura.Armazenamento.Abstrações.Queryable.Leitura;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Infraestrutura.Armazenamento.Memória
{
    public sealed partial class DaoMemória : IToAsyncEnumerable
    {
        public async IAsyncEnumerable<T> ToAsyncEnumerable<T>(IQueryable<T> query,
            [EnumeratorCancellation] CancellationToken cancellationToken) where T : class, new()
        {
            foreach (var registro in query)
            {
                cancellationToken.ThrowIfCancellationRequested();
                yield return await Task.FromResult(registro);
            }
        }
    }
}
