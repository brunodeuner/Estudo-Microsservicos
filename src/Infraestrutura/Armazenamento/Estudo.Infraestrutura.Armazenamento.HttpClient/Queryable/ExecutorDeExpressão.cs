using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Infraestrutura.Armazenamento.HttpClient.Queryable
{
    public class ExecutorExpressao
    {
        private readonly ConfiguraçãoDoDaoHttpClient configuraçãoDoDaoHttpClient;
        private readonly ExecutorDeRequisições executorDeRequisições;

        public ExecutorExpressao(ConfiguraçãoDoDaoHttpClient configuraçãoDoDaoHttpClient,
            ExecutorDeRequisições executorDeRequisições)
        {
            this.configuraçãoDoDaoHttpClient = configuraçãoDoDaoHttpClient;
            this.executorDeRequisições = executorDeRequisições;
        }

        public Task<List<T>> ExecutarRequisicao<T>(Expression expressão, CancellationToken cancellationToken)
        {
            var rota = ObterRota<T>(expressão as MethodCallExpression);
            return executorDeRequisições.ExecutarRequisiçãoEObterResposta<T, List<T>>(new Dtos.DadosDaRequisição<T>(
                HttpMethod.Get), rota, cancellationToken);
        }

        private Uri ObterRota<T>(MethodCallExpression methodCallExpression)
        {
            var urlRequisição = new StringBuilder(
                configuraçãoDoDaoHttpClient.ObterRotaAPartirDoTipo(typeof(T)).AbsoluteUri);
            var tradutorOData = new TradutorOData();
            var odata = methodCallExpression is null
                ? string.Empty
                : tradutorOData.ObterODataAPartirDaExpressao(methodCallExpression);
            if (!string.IsNullOrEmpty(odata))
            {
                urlRequisição.Append('?');
                urlRequisição.Append(odata);
            }
            return new(urlRequisição.ToString());
        }
    }
}
