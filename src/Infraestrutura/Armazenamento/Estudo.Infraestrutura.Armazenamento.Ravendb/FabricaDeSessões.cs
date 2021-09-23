using Raven.Client.Documents;
using Raven.Client.Documents.Conventions;
using Raven.Client.Documents.Session;
using System;

namespace Estudo.Infraestrutura.Armazenamento.Ravendb
{
    public sealed class FabricaDeSessões : IDisposable
    {
        private readonly Configuração configuração;
        private IDocumentStore documentStore;

        public FabricaDeSessões(Configuração configuração) => this.configuração = configuração;

        public IAsyncDocumentSession CriarSessaoAssincrona() => ObterDocumentStore().OpenAsyncSession();

        public void Dispose()
        {
            documentStore?.Dispose();
            documentStore = default;
        }

        private IDocumentStore ObterDocumentStore() => documentStore ??= CriarEIniciarDocumentStore();

        private IDocumentStore CriarEIniciarDocumentStore()
        {
            var novoDocumentStore = new DocumentStore
            {
                Certificate = configuração.Certificado,
                Urls = configuração.UrlsConnection,
                Database = configuração.Database,
                Conventions = CriarConvenções()
            };
            novoDocumentStore.Initialize();
            return novoDocumentStore;
        }

        private DocumentConventions CriarConvenções() => new()
        {
            IdentityPartsSeparator = '-',
            AggressiveCache = {
               Duration = configuração.CacheAgressivo.Duration,
               Mode = configuração.CacheAgressivo.Mode,
            },
        };
    }
}
