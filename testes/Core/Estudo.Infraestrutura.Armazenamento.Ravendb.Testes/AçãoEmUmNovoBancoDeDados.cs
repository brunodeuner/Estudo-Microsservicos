using Raven.Client.Documents;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;
using System;
using System.Threading.Tasks;

namespace Estudo.Infraestrutura.Armazenamento.Ravendb.Testes
{
    public static class AçãoEmUmNovoBancoDeDados
    {
        public static async Task ExecutarTarefaEmUmNovoBancoDeDados(this IDocumentStore documentStore,
            ConfiguraçãoDoRavendb configuraçãoDoRavendb, Func<Task> tarefa)
        {
            documentStore.RemoverDatabase(configuraçãoDoRavendb);
            documentStore.CriarDatabase(configuraçãoDoRavendb);
            try
            {
                await tarefa();
            }
            finally
            {
                documentStore.RemoverDatabase(configuraçãoDoRavendb);
            }
        }

        private static void CriarDatabase(this IDocumentStore documentStore, ConfiguraçãoDoRavendb configuraçãoDoRavendb)
            => documentStore.Maintenance.Server.Send(
               new CreateDatabaseOperation(new DatabaseRecord(configuraçãoDoRavendb.Database)));

        private static void RemoverDatabase(this IDocumentStore documentStore,
            ConfiguraçãoDoRavendb configuraçãoDoRavendb) => documentStore.Maintenance.Server.Send(
                new DeleteDatabasesOperation(configuraçãoDoRavendb.Database, true));
    }
}
