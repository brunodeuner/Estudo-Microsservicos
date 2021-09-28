using Estudo.Infraestrutura.Armazenamento.Ravendb;
using Raven.Client.Documents;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Infraestrutura.Bus.Ravendb.Testes
{
    internal static class CriarSubscriptionsNãoExistentes
    {
        private const string NomeDaInscrição = nameof(EntidadeDeTeste);

        public static async Task CriarSubscriptionParaEntidadeDeTeste(this IDocumentStore documentStore,
            ConfiguraçãoDoRavendb configuraçãoDoRavendb, CancellationToken cancellationToken)
        {
            await documentStore.Subscriptions.CreateAsync(
               new Raven.Client.Documents.Subscriptions.SubscriptionCreationOptions<EntidadeDeTeste>()
               {
                   Name = NomeDaInscrição
               }, configuraçãoDoRavendb.Database, token: cancellationToken);
        }

        public static Task RemoverSubscriptionDeEntidadeDeTeste(this IDocumentStore documentStore,
            CancellationToken cancellationToken) =>
            documentStore.Subscriptions.DeleteAsync(NomeDaInscrição, token: cancellationToken);
    }
}
