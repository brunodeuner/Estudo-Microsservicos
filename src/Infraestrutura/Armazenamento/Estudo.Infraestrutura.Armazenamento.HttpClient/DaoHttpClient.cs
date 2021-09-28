using Estudo.Infraestrutura.Armazenamento.Abstrações;
using Estudo.Infraestrutura.Armazenamento.Abstrações.Queryable;
using Estudo.Infraestrutura.Armazenamento.HttpClient.Dtos;
using Estudo.Infraestrutura.Armazenamento.HttpClient.Queryable;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Infraestrutura.Armazenamento.HttpClient
{
    public sealed partial class DaoHttpClient : IDao
    {
        private readonly ExecutorDeRequisições executorDeRequisições;
        private readonly ExecutorExpressao executorExpressao;

        public DaoHttpClient(ExecutorDeRequisições executorDeRequisições, ExecutorExpressao executorExpressao)
        {
            this.executorDeRequisições = executorDeRequisições;
            this.executorExpressao = executorExpressao;
        }

        public async ValueTask Salvar<T>(T objeto, CancellationToken cancellationToken) where T : class, new() =>
            await executorDeRequisições.ExecutarRequisição(
                new DadosDaRequisição<T>(HttpMethod.Post, objeto), cancellationToken);

        public async ValueTask<T> ObterPeloId<T>(string id, CancellationToken cancellationToken) =>
            await executorDeRequisições.ExecutarRequisição<T, T>(
                new DadosDaRequisição<T>(HttpMethod.Get, id), cancellationToken);

        public ValueTask SalvarAlterações(CancellationToken cancellationToken) => ValueTask.CompletedTask;

        public IQueryable<T> Selecionar<T>()
        {
            var query = Array.Empty<T>().AsQueryable();
            return new QueryableDao<T>(query, new QueryProviderDao(query.Provider, this));
        }
    }
}
