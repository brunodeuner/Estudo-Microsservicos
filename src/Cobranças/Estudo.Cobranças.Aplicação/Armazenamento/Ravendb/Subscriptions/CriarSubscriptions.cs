using Estudo.Cobranças.Aplicação.Armazenamento.Consumidores.Eventos;
using Estudo.Core.Infraestrutura.Armazenamento.Ravendb;
using Raven.Client.Documents;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Cobranças.Aplicação.Armazenamento.Ravendb.Subscriptions
{
    internal static class CriarSubscriptionsNãoExistentes
    {
        private const int QuantidadeDeSubscriptions = 1;

        public static async Task CriarSubscriptions(this IDocumentStore documentStore,
            ConfiguraçãoDoRavendb configuraçãoDoRavendb, CancellationToken cancellationToken)
        {
            var subscriptions = await documentStore.Subscriptions.GetSubscriptionsAsync(
                0, QuantidadeDeSubscriptions, configuraçãoDoRavendb.Database, token: cancellationToken);

            await CriarSubscriptionSeNãoExistir<Cliente>(documentStore, configuraçãoDoRavendb,
                subscriptions, cancellationToken);
        }

        private static async Task CriarSubscriptionSeNãoExistir<T>(IDocumentStore documentStore,
            ConfiguraçãoDoRavendb configuraçãoDoRavendb,
            IEnumerable<Raven.Client.Documents.Subscriptions.SubscriptionState> subscriptions,
            CancellationToken cancellationToken)
        {
            var nomeDaInscrição = typeof(T).Name;
            if (!subscriptions.Any(x => x.SubscriptionName == nomeDaInscrição))
                await documentStore.Subscriptions.CreateAsync(
                    new Raven.Client.Documents.Subscriptions.SubscriptionCreationOptions<T>()
                    {
                        Name = nomeDaInscrição
                    }, configuraçãoDoRavendb.Database, token: cancellationToken);
        }
    }
}
