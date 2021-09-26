using Raven.Client.Documents;
using Raven.Client.Documents.Conventions;
using Raven.Client.Documents.Session;
using System;

namespace Estudo.Infraestrutura.Armazenamento.Ravendb
{
    public sealed class FabricaDoRavendb : IDisposable
    {
        private readonly ConfiguraçãoDoRavendb configuraçãoDoRavendb;

        public FabricaDoRavendb(ConfiguraçãoDoRavendb configuraçãoDoRavendb) =>
            this.configuraçãoDoRavendb = configuraçãoDoRavendb;

        public IDocumentStore DocumentStore { get; private set; }

        public IAsyncDocumentSession CriarSessaoAssincrona() => ObterDocumentStore().OpenAsyncSession();

        public void Dispose()
        {
            DocumentStore?.Dispose();
            DocumentStore = default;
        }

        private IDocumentStore ObterDocumentStore() => DocumentStore ??= CriarEIniciarDocumentStore();

        private IDocumentStore CriarEIniciarDocumentStore()
        {
            var novoDocumentStore = new DocumentStore
            {
                Certificate = configuraçãoDoRavendb.Certificado,
                Urls = configuraçãoDoRavendb.UrlsConnection,
                Database = configuraçãoDoRavendb.Database,
                Conventions = CriarConvenções()
            };
            novoDocumentStore.Initialize();
            return novoDocumentStore;
        }

        private DocumentConventions CriarConvenções() => new()
        {
            IdentityPartsSeparator = '-',
            AggressiveCache = {
               Duration = configuraçãoDoRavendb.CacheAgressivo.Duration,
               Mode = configuraçãoDoRavendb.CacheAgressivo.Mode,
            },
        };
    }
}
