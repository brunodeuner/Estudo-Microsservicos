using Estudo.Infraestrutura.Armazenamento.HttpClient.Dtos;
using Estudo.Infraestrutura.Geral;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Infraestrutura.Armazenamento.HttpClient
{
    public class ExecutorDeRequisições
    {
        private readonly System.Net.Http.HttpClient httpClient;
        private readonly JsonSerializer jsonSerializer;

        public ExecutorDeRequisições(System.Net.Http.HttpClient httpClient,
            ConfiguraçãoDoDaoHttpClient configuraçãoDoDaoHttpClient)
        {
            this.httpClient = httpClient;
            ConfiguraçãoDoDaoHttpClient = configuraçãoDoDaoHttpClient;
            jsonSerializer = JsonSerializer.Create(configuraçãoDoDaoHttpClient.JsonDeserializerSettings);
        }

        public ConfiguraçãoDoDaoHttpClient ConfiguraçãoDoDaoHttpClient { get; set; }

        public Task ExecutarRequisição<Envio>(DadosDaRequisição<Envio> dadosDaRequisição,
            CancellationToken cancellationToken) =>
            ExecutarRequisiçãoInterna(dadosDaRequisição, cancellationToken);

        public Task<Resposta> ExecutarRequisição<Envio, Resposta>(DadosDaRequisição<Envio> dadosDaRequisição,
            CancellationToken cancellationToken) =>
            ExecutarRequisiçãoEObterResposta<Envio, Resposta>(dadosDaRequisição, cancellationToken);

        public Task<Resposta> ExecutarRequisiçãoEObterResposta<Envio, Resposta>(
          DadosDaRequisição<Envio> dadosDaRequisição, CancellationToken cancellationToken)
        {
            var rota = ObterRota(dadosDaRequisição);
            return ExecutarRequisiçãoEObterResposta<Envio, Resposta>(dadosDaRequisição, rota, cancellationToken);
        }

        public async Task<Resposta> ExecutarRequisiçãoEObterResposta<Envio, Resposta>(
            DadosDaRequisição<Envio> dadosDaRequisição, Uri rota, CancellationToken cancellationToken)
        {
            using var resposta = await ExecutarRequisiçãoInterna(dadosDaRequisição, rota, cancellationToken);
            return ConverterRespostaDaRequisiçãoNoTipoEsperado<Resposta>(
                await resposta.Content.ReadAsStreamAsync(cancellationToken));
        }

        private Task<HttpResponseMessage> ExecutarRequisiçãoInterna<Envio>(
            DadosDaRequisição<Envio> dadosDaRequisição, CancellationToken cancellationToken)
        {
            var rota = ObterRota(dadosDaRequisição);
            return ExecutarRequisiçãoInterna(dadosDaRequisição, rota, cancellationToken);
        }

        private async Task<HttpResponseMessage> ExecutarRequisiçãoInterna<Envio>(DadosDaRequisição<Envio> dadosDaRequisição,
            Uri rota, CancellationToken cancellationToken)
        {
            var requisição = ObterRequisição(dadosDaRequisição, rota);
            var resposta = await httpClient.SendAsync(requisição, cancellationToken);
            return resposta.EnsureSuccessStatusCode();
        }

        private Resposta ConverterRespostaDaRequisiçãoNoTipoEsperado<Resposta>(Stream resposta)
        {
            using var streamReader = new StreamReader(resposta);
            using var reader = new JsonTextReader(streamReader);
            return jsonSerializer.Deserialize<Resposta>(reader);
        }

        private HttpRequestMessage ObterRequisição<Envio>(DadosDaRequisição<Envio> dadosDaRequisição, Uri rota)
        {
            var requisição = new HttpRequestMessage(dadosDaRequisição.HttpMethod, rota);
            if (dadosDaRequisição.Corpo is not null)
                requisição.Content = new StringContent(JsonConvert.SerializeObject(dadosDaRequisição.Corpo,
                    ConfiguraçãoDoDaoHttpClient.JsonSerializerSettings), Encoding.UTF8, MediaTypeNames.Application.Json);
            return requisição;
        }

        private Uri ObterRota<Envio>(DadosDaRequisição<Envio> dadosDaRequisição)
        {
            var rota = ConfiguraçãoDoDaoHttpClient.ObterRotaAPartirDoTipo(typeof(Envio));
            if (dadosDaRequisição.Id.Preenchido())
                return new Uri(rota, dadosDaRequisição.Id);
            return rota;
        }
    }
}
