using Estudo.Infraestrutura.Armazenamento.Abstrações.Queryable.Leitura;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Estudo.Infraestrutura.Armazenamento.HttpClient
{
    public partial class DaoHttpClient : IToAsyncEnumerable
    {
        public async IAsyncEnumerable<T> ToAsyncEnumerable<T>(IQueryable<T> query,
            [EnumeratorCancellation] CancellationToken cancellationToken) where T : class, new()
        {
            var quantidadeDeRegistrosPorPaginação =
                executorDeRequisições.ConfiguraçãoDoDaoHttpClient.QuantidadeDeRegistrosPorPaginação;

            var registrosAPular = 0;
            while (!cancellationToken.IsCancellationRequested)
            {
                query = query.Skip(registrosAPular).Take(quantidadeDeRegistrosPorPaginação);
                var resultados = await executorExpressao.ExecutarRequisicao<T>(query.Expression, cancellationToken);
                var quantidadeDeRegistrosProcessados = 0;
                foreach (var resultado in resultados)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    quantidadeDeRegistrosProcessados += 1;
                    yield return resultado;
                }
                if (quantidadeDeRegistrosProcessados < quantidadeDeRegistrosPorPaginação)
                    break;
                registrosAPular += quantidadeDeRegistrosPorPaginação;
            }
        }
    }
}
