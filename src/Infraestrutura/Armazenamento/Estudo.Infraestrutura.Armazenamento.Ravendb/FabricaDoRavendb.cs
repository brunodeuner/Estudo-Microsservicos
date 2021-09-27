using Raven.Client.Documents;
using Raven.Client.Documents.Conventions;
using Raven.Client.Documents.Session;
using System;

namespace Estudo.Infraestrutura.Armazenamento.Ravendb
{
    public sealed class FabricaDoRavendb : IDisposable
    {
        private readonly ConfiguraçãoDoRavendb configuraçãoDoRavendb;
        private IDocumentStore documentStore;
        private IDisposable cacheAgresivo;

        public FabricaDoRavendb(ConfiguraçãoDoRavendb configuraçãoDoRavendb) =>
            this.configuraçãoDoRavendb = configuraçãoDoRavendb;

        public IAsyncDocumentSession CriarSessaoAssincrona() => ObterDocumentStore().OpenAsyncSession();

        public void Dispose()
        {
            documentStore?.Dispose();
            documentStore = default;
            cacheAgresivo?.Dispose();
            cacheAgresivo = default;
        }

        private IDocumentStore ObterDocumentStore() => documentStore ??= CriarEIniciarDocumentStore();

        private IDocumentStore CriarEIniciarDocumentStore()
        {
            var novoDocumentStore = new DocumentStore
            {
                Certificate = configuraçãoDoRavendb.ObterCertificado(),
                Urls = configuraçãoDoRavendb.UrlsConnection,
                Database = configuraçãoDoRavendb.Database,
                Conventions = CriarConvenções()
            };
            novoDocumentStore.Initialize();
            if (configuraçãoDoRavendb.TempoDeDuraçãoDoCache.HasValue)
                cacheAgresivo = novoDocumentStore.AggressivelyCache();
            return novoDocumentStore;
        }

        private DocumentConventions CriarConvenções()
        {
            var convenções = new DocumentConventions()
            {
                IdentityPartsSeparator = '-',
            };
            if (configuraçãoDoRavendb.TempoDeDuraçãoDoCache.HasValue)
            {
                convenções.AggressiveCache.Duration = configuraçãoDoRavendb.TempoDeDuraçãoDoCache.Value;
                convenções.AggressiveCache.Mode = Raven.Client.Http.AggressiveCacheMode.TrackChanges;
            };
            return convenções;
        }
    }
}
