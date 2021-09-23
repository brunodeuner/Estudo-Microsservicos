using Estudo.Infraestrutura.Armazenamento.Abstrações.Queryable.Leitura;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Estudo.Infraestrutura.Armazenamento.Ravendb
{
    internal partial class DaoRavendb : IToAsyncEnumerable
    {
        public async IAsyncEnumerable<T> ToAsyncEnumerable<T>(IQueryable<T> query,
            [EnumeratorCancellation] CancellationToken cancellationToken) where T : class, new()
        {
            var queryRaven = (RavenQueryInspector<T>)query;
            var asyncDocumentSession = (IAsyncDocumentSession)queryRaven.Session;
            await using var enumerator = await asyncDocumentSession.Advanced.StreamAsync(query, cancellationToken);
            while (await enumerator.MoveNextAsync())
                yield return enumerator.Current.Document;
        }
    }
}
