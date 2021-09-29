using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.CálculoDeConsumo.Infraestrutura
{
    public static class ProcessarRegistrosEmLote
    {
        public static async Task Processar<T>(IAsyncEnumerable<T> registros, Func<T, CancellationToken, Task> ação,
            CancellationToken cancellationToken)
        {
            var registrosSendoProcessados = new List<Task>();
            await foreach (var registro in registros)
            {
                registrosSendoProcessados.RemoveAll(x => x.IsCompleted);
                registrosSendoProcessados.Add(ação(registro, cancellationToken));
            }
            await Task.WhenAll(registrosSendoProcessados);
        }
    }
}
