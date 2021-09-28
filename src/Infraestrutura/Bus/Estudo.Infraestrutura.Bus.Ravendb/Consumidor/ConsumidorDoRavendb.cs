using Estudo.Infraestrutura.Armazenamento.Ravendb;
using Estudo.Infraestrutura.Bus.Abstrações;
using Estudo.Infraestrutura.Bus.Abstrações.Consumidor;
using Estudo.Infraestrutura.Bus.Abstrações.Consumidor.Dtos;
using Raven.Client.Documents;
using Raven.Client.Documents.Subscriptions;
using Raven.Client.Exceptions.Documents.Subscriptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Infraestrutura.Bus.Ravendb.Consumidor
{
    public class ConsumidorDoRavendb<T> : IConsumidor<T> where T : class, new()
    {
        private readonly IDocumentStore documentStore;
        private readonly ConfiguraçãoDoRavendb configuraçãoDoRavendb;
        private readonly bool pararAoConsumirTodosOsEventos;

        public ConsumidorDoRavendb(IDocumentStore documentStore, ConfiguraçãoDoRavendb configuraçãoDoRavendb,
            bool pararAoConsumirTodosOsEventos = false)
        {
            this.documentStore = documentStore;
            this.configuraçãoDoRavendb = configuraçãoDoRavendb;
            this.pararAoConsumirTodosOsEventos = pararAoConsumirTodosOsEventos;
        }

        public event EventoAssíncrono<EventoEventArgs<T>> Consumir;

        public async Task Iniciar(string identificador, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using var subscription = ObterSubscription(identificador);
                try
                {
                    await subscription.Run(loteDeProcessamento => ConsumirLote(loteDeProcessamento, cancellationToken),
                        cancellationToken);
                }
                catch (Exception e)
                {
                    if (pararAoConsumirTodosOsEventos && e is SubscriptionClosedException)
                        break;

                    throw;
                }
            }
        }

        private async Task ConsumirLote(SubscriptionBatch<T> loteDeProcessamento, CancellationToken cancellationToken)
        {
            foreach (var itens in loteDeProcessamento.Items)
                await Consumir(new EventoEventArgs<T>(itens.Result), cancellationToken);
        }

        private SubscriptionWorker<T> ObterSubscription(string identificador)
        {
            var configuraçãoWorker = new SubscriptionWorkerOptions(identificador)
            {
                CloseWhenNoDocsLeft = pararAoConsumirTodosOsEventos
            };
            return documentStore.Subscriptions.GetSubscriptionWorker<T>(
                configuraçãoWorker, database: configuraçãoDoRavendb.Database);
        }
    }
}
