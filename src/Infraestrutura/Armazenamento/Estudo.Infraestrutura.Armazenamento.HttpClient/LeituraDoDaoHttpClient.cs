using Estudo.Infraestrutura.Armazenamento.Abstrações.Queryable;
using Estudo.Infraestrutura.Armazenamento.Abstrações.Queryable.Leitura;
using Estudo.Infraestrutura.Armazenamento.HttpClient.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Infraestrutura.Armazenamento.HttpClient
{
    public partial class DaoHttpClient : IToAsyncEnumerable
    {
        private const int QuantidadeDeRegistrosPorPaginação = 50;

        public async IAsyncEnumerable<T> ToAsyncEnumerable<T>(IQueryable<T> query,
            [EnumeratorCancellation] CancellationToken cancellationToken) where T : class, new()
        {
            var registrosAPular = 0;
            while (!cancellationToken.IsCancellationRequested)
            {
                query = query.Skip(registrosAPular).Take(QuantidadeDeRegistrosPorPaginação);
                var resultados = await ExecutarRequisição(query, cancellationToken);
                var quantidadeDeRegistrosProcessados = 0;
                foreach (var resultado in resultados)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    quantidadeDeRegistrosProcessados += 1;
                    yield return resultado;
                }
                if (quantidadeDeRegistrosProcessados < QuantidadeDeRegistrosPorPaginação)
                    break;
                registrosAPular += QuantidadeDeRegistrosPorPaginação;
            }
        }

        private static Task<IEnumerable<T>> ExecutarRequisição<T>(IQueryable<T> query,
            CancellationToken cancellationToken) where T : class, new()
        {
            var daoHttpClient = (DaoHttpClient)((QueryProviderDao)query.Provider).Dao;
            return daoHttpClient.ExecutorDeRequisições.ExecutarRequisição<T, IEnumerable<T>>(
                new DadosDaRequisição<T>(HttpMethod.Get), cancellationToken);
        }
    }
}
