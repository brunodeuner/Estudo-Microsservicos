using Estudo.Core.Infraestrutura.Armazenamento.Abstrações;
using Estudo.Core.Infraestrutura.Armazenamento.Abstrações.Queryable;
using Raven.Client.Documents.Session;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Core.Infraestrutura.Armazenamento.Ravendb
{
    public sealed partial class DaoRavendb : IDao, IDisposable
    {
        private readonly FabricaDoRavendb fabricaDeSessões;
        private IAsyncDocumentSession sessão;

        public DaoRavendb(FabricaDoRavendb fabricaDeSessões) => this.fabricaDeSessões = fabricaDeSessões;

        public async ValueTask Salvar<T>(T objeto, CancellationToken cancellationToken) where T : class, new() =>
            await ObterSessão().StoreAsync(objeto, cancellationToken);

        public async ValueTask<T> ObterPeloId<T>(string id, CancellationToken cancellationToken) =>
            await ObterSessão().LoadAsync<T>(id, cancellationToken);

        public IQueryable<T> Selecionar<T>()
        {
            var query = ObterSessão().Query<T>();
            return new QueryableDao<T>(query, new QueryProviderDao(query.Provider, this));
        }

        public async ValueTask SalvarAlterações(CancellationToken cancellationToken)
        {
            if (sessão is not null)
                await sessão.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            sessão?.Dispose();
            sessão = null;
        }

        private IAsyncDocumentSession ObterSessão() =>
            sessão ??= fabricaDeSessões.CriarSessaoAssincrona();
    }
}
