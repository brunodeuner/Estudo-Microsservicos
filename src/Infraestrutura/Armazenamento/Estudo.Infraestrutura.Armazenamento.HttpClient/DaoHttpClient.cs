using Estudo.Infraestrutura.Armazenamento.Abstrações;
using Estudo.Infraestrutura.Armazenamento.Abstrações.Queryable;
using Estudo.Infraestrutura.Armazenamento.HttpClient.Dtos;
using Estudo.Infraestrutura.Armazenamento.HttpClient.Queryable;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Infraestrutura.Armazenamento.HttpClient
{
    public sealed partial class DaoHttpClient : IDao
    {
        public DaoHttpClient(ExecutorDeRequisições executorDeRequisições) =>
            ExecutorDeRequisições = executorDeRequisições;

        public ExecutorDeRequisições ExecutorDeRequisições { get; }

        public async ValueTask Adicionar<T>(T objeto, CancellationToken cancellationToken) where T : class, new() =>
            await ExecutorDeRequisições.ExecutarRequisição(
                new DadosDaRequisição<T>(HttpMethod.Post, objeto), cancellationToken);

        public async ValueTask Atualizar<T>(T objeto, CancellationToken cancellationToken) where T : class, new() =>
            await ExecutorDeRequisições.ExecutarRequisição(
                new DadosDaRequisição<T>(HttpMethod.Put, objeto), cancellationToken);

        public async ValueTask<T> ObterPeloId<T>(string id, CancellationToken cancellationToken) =>
            await ExecutorDeRequisições.ExecutarRequisição<T, T>(
                new DadosDaRequisição<T>(HttpMethod.Get, id), cancellationToken);

        public async ValueTask Remover<T>(T objeto, CancellationToken cancellationToken) where T : class, new() =>
            await ExecutorDeRequisições.ExecutarRequisição(
                new DadosDaRequisição<T>(HttpMethod.Delete, objeto), cancellationToken);

        public ValueTask SalvarAlterações(CancellationToken cancellationToken) => ValueTask.CompletedTask;

        public IQueryable<T> Selecionar<T>()
        {
            var query = new QueryableEmBranco<T>();
            return new QueryableDao<T>(query, new QueryProviderDao(query.Provider, this));
        }
    }
}
