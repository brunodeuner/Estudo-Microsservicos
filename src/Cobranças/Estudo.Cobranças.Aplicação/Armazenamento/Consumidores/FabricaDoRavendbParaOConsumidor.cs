using Estudo.Core.Infraestrutura.Armazenamento.Ravendb;
using Raven.Client.Documents;
using System;

namespace Estudo.Cobranças.Aplicação.Armazenamento.Consumidores
{
    internal sealed class FabricaDoRavendbParaOConsumidor : IDisposable
    {
        private FabricaDoRavendb fabricaDoRavendb;

        public FabricaDoRavendbParaOConsumidor(FabricaDoRavendb fabricaDoRavendb) =>
            this.fabricaDoRavendb = fabricaDoRavendb;

        public IDocumentStore ObterDocumentStore() => fabricaDoRavendb.DocumentStore;

        public void Dispose()
        {
            fabricaDoRavendb?.Dispose();
            fabricaDoRavendb = default;
        }
    }
}
