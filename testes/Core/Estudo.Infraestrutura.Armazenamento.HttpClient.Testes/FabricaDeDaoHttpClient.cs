using Estudo.Infraestrutura.Armazenamento.Abstrações;
using Estudo.Infraestrutura.Armazenamento.HttpClient.Queryable;

namespace Estudo.Infraestrutura.Armazenamento.HttpClient.Testes
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
