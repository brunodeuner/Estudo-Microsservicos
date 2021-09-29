using Raven.Client.Documents;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Cobranças.Aplicação.Armazenamento.Ravendb.MapReduces
{
    public static class CriarMapReducesNecessáriosParaAAplicação
    {
        public static Task CriarMapReduces(this IDocumentStore documentStore,
            CancellationToken cancellationToken) => new MapReduceDeCobrançasPorMêsEEstado().ExecuteAsync(documentStore,
                token: cancellationToken);
    }
}
