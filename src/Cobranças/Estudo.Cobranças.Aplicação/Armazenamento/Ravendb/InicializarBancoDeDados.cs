using Estudo.Cobranças.Aplicação.Armazenamento.Ravendb.MapReduces;
using Estudo.Cobranças.Aplicação.Armazenamento.Ravendb.Subscriptions;
using Estudo.Core.Infraestrutura.Armazenamento.Ravendb;
using Raven.Client.Documents;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Cobranças.Aplicação.Armazenamento.Ravendb
{
    public static class InicializarBancoDeDados
    {
        public static async Task Inicializar(this IDocumentStore documentStore,
            ConfiguraçãoDoRavendb configuraçãoDoRavendb, CancellationToken cancellationToken)
        {
            await documentStore.CriarMapReduces(cancellationToken);
            await documentStore.CriarSubscriptions(configuraçãoDoRavendb, cancellationToken);
        }
    }
}
