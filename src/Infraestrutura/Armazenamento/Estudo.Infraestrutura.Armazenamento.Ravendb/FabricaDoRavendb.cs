using Raven.Client.Documents;
using Raven.Client.Documents.Conventions;
using Raven.Client.Documents.Session;
using System;

namespace Estudo.Infraestrutura.Armazenamento.Ravendb
{
    public sealed class FabricaDoRavendb : IDisposable
    {
        private IDocumentStore documentStore;
        private IDisposable cacheAgresivo;

        public FabricaDoRavendb(ConfiguraçãoDoRavendb configuraçãoDoRavendb) =>
            ConfiguraçãoDoRavendb = configuraçãoDoRavendb;

        public ConfiguraçãoDoRavendb ConfiguraçãoDoRavendb { get; init; }
        public IDocumentStore DocumentStore { get => documentStore ??= CriarEIniciarDocumentStore(); }

        public IAsyncDocumentSession CriarSessaoAssincrona() => DocumentStore.OpenAsyncSession();

        public void Dispose()
        {
            documentStore?.Dispose();
            documentStore = default;
            cacheAgresivo?.Dispose();
            cacheAgresivo = default;
        }

        private IDocumentStore CriarEIniciarDocumentStore()
        {
            var novoDocumentStore = new DocumentStore
            {
                Certificate = ConfiguraçãoDoRavendb.ObterCertificado(),
                Urls = ConfiguraçãoDoRavendb.UrlsConnection,
                Database = ConfiguraçãoDoRavendb.Database,
                Conventions = CriarConvenções()
            };
            novoDocumentStore.Initialize();
            if (ConfiguraçãoDoRavendb.TempoDeDuraçãoDoCache.HasValue)
                cacheAgresivo = novoDocumentStore.AggressivelyCache();
            return novoDocumentStore;
        }

        private DocumentConventions CriarConvenções()
        {
            var convenções = new DocumentConventions()
            {
                IdentityPartsSeparator = '-',
            };
            if (ConfiguraçãoDoRavendb.TempoDeDuraçãoDoCache.HasValue)
            {
                convenções.AggressiveCache.Duration = ConfiguraçãoDoRavendb.TempoDeDuraçãoDoCache.Value;
                convenções.AggressiveCache.Mode = Raven.Client.Http.AggressiveCacheMode.TrackChanges;
            };
            return convenções;
        }
    }
}
