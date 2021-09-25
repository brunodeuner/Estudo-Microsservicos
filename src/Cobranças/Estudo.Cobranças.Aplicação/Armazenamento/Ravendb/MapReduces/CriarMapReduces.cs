﻿using Raven.Client.Documents;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Cobranças.Aplicação.Armazenamento.Ravendb.MapReduces
{
    public static class CriarMapReduces
    {
        public static Task CriarTodosOsMapReduce(this IDocumentStore documentStore,
            CancellationToken cancellationToken) => new CobrançasPorMêsEEstado().ExecuteAsync(documentStore,
                token: cancellationToken);
    }
}
