using Raven.Client.Documents;
using Raven.Client.Documents.Conventions;
using Raven.Client.Documents.Session;
using System;

namespace Estudo.Core.Infraestrutura.Armazenamento.Ravendb
{
    public sealed class FabricaDoRavendb : IDisposable
    {
        private IDocumentStore documentStore;

        public FabricaDoRavendb(ConfiguraçãoDoRavendb configuraçãoDoRavendb) =>
            ConfiguraçãoDoRavendb = configuraçãoDoRavendb;

        public ConfiguraçãoDoRavendb ConfiguraçãoDoRavendb { get; init; }
        public IDocumentStore DocumentStore { get => documentStore ??= CriarEIniciarDocumentStore(); }

        public IAsyncDocumentSession CriarSessaoAssincrona() => DocumentStore.OpenAsyncSession();

        public void Dispose()
        {
            documentStore?.Dispose();
            documentStore = default;
            GC.SuppressFinalize(this);
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
            return novoDocumentStore;
        }

        private static DocumentConventions CriarConvenções() => new()
        {
            IdentityPartsSeparator = '-',
            FindCollectionName = tipo => tipo.Name,
        };
    }
}
