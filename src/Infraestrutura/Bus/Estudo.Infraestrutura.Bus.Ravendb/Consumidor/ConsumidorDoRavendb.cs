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

        public ConsumidorDoRavendb(IDocumentStore documentStore, ConfiguraçãoDoRavendb configuraçãoDoRavendb)
        {
            this.documentStore = documentStore;
            this.configuraçãoDoRavendb = configuraçãoDoRavendb;
        }

        public event EventoAssíncrono<EventoEventArgs<T>> Consumir;
        public event EventoAssíncrono<ExceçãoEventArgs> Exceção;

        public async Task Iniciar(string identificador, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using var subscription = ObterSubscription(identificador);

                try
                {
                    subscription.OnSubscriptionConnectionRetry += async exception =>
                        await Exceção(new ExceçãoEventArgs(exception), cancellationToken);

                    await subscription.Run(loteDeProcessamento => ConsumirLote(loteDeProcessamento, cancellationToken),
                        cancellationToken);
                }
                catch (Exception e)
                {
                    if (cancellationToken.IsCancellationRequested)
                        throw;

                    if (e is SubscriptionClosedException)
                        return;

                    if (e is SubscriptionDoesNotBelongToNodeException)
                        continue;

                    await ProcessarExceção(e, cancellationToken);
                }
            }
        }

        private async Task ConsumirLote(SubscriptionBatch<T> loteDeProcessamento, CancellationToken cancellationToken)
        {
            foreach (var itens in loteDeProcessamento.Items)
            {
                try
                {
                    await Consumir(new EventoEventArgs<T>(itens.Result), cancellationToken);
                }
                catch (Exception e)
                {
                    await ProcessarExceção(e, cancellationToken);
                    if (cancellationToken.IsCancellationRequested)
                        break;
                }
            }
        }

        private SubscriptionWorker<T> ObterSubscription(string identificador)
        {
            var configuraçãoWorker = new SubscriptionWorkerOptions(identificador);
            return documentStore.Subscriptions.GetSubscriptionWorker<T>(
                configuraçãoWorker, database: configuraçãoDoRavendb.Database);
        }

        private Task ProcessarExceção(Exception exceção, CancellationToken cancellationToken)
        {
            if (Exceção is not null)
                return Exceção(new ExceçãoEventArgs(exceção), cancellationToken);
            return Task.CompletedTask;
        }
    }
}
