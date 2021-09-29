using Estudo.Core.Infraestrutura.Armazenamento.Abstrações;
using Estudo.Core.Infraestrutura.Armazenamento.HttpClient.Queryable;

namespace Estudo.Core.Infraestrutura.Armazenamento.HttpClient.Testes
{
    internal class FabricaDeDaoHttpClient
    {
        public static IDao ObterDao(System.Net.Http.HttpClient httpClient,
            ConfiguraçãoDoDaoHttpClient configuraçãoDoDaoHttpClient)
        {
            var executorDeRequisições = new ExecutorDeRequisições(httpClient, configuraçãoDoDaoHttpClient);
            return new DaoHttpClient(executorDeRequisições,
                new ExecutorExpressao(configuraçãoDoDaoHttpClient, executorDeRequisições));
        }
    }
}
