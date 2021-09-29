using Estudo.Core.Http.Testes.Arquivos;
using Estudo.Core.Http.Testes.Dtos;
using Estudo.Core.Http.Testes.Variáveis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Estudo.Core.Http.Testes
{
    public static class ExecutorDeTestes
    {
        public static async Task ExecutarTodosOsTestes(ConfiguraçãoDosTestes configuraçãoDosTestes,
            CancellationToken cancellationToken = default)
        {
            var caminhosDosJsonsDeTeste = ObtençãoDeArquivos.ObterArquivosOrdenados(Path.Combine(
                LocalAtualDeExecução.ObterLocalAtualDeExecução(), configuraçãoDosTestes.Diretorio), "*.json");
            foreach (var caminhoDoJsonDeTeste in caminhosDosJsonsDeTeste)
                await ExecutarTeste(configuraçãoDosTestes, caminhoDoJsonDeTeste, cancellationToken);
        }

        private static async Task ExecutarTeste(ConfiguraçãoDosTestes configuraçãoDosTestes,
            string caminhoDoJsonDeTeste, CancellationToken cancellationToken = default)
        {
            var cenário = await ObtemCenário(caminhoDoJsonDeTeste);
            for (var i = 0; i < cenário.Comandos.Count; i++)
            {
                var comandoAExecutar = ObterComandoAExecutar(cenário, i);
                await ExecutarComando(configuraçãoDosTestes.FabricaHttpClient, $"Erro: {cenário.Nome} - comando {i}",
                    comandoAExecutar, cancellationToken);
            }
        }

        private static Comando ObterComandoAExecutar(Cenário cenário, int i)
        {
            var comandoAExecutarJson = JsonConvert.SerializeObject(cenário.Comandos[i]);
            comandoAExecutarJson = comandoAExecutarJson.SubstituirVariável();
            return JsonConvert.DeserializeObject<Comando>(comandoAExecutarJson);
        }

        private static async Task ExecutarComando(Func<Comando, HttpClient> fabricaHttpClient, string mensagemErro,
            Comando comando, CancellationToken cancellationToken)
        {
            var httpClient = fabricaHttpClient(comando);
            var httpRequestMessage = CriarHttpRequestMessageAPartirDoComando(comando);
            var respostaRequisicao = await httpClient.SendAsync(httpRequestMessage, cancellationToken);
            var respostaTextoRequisicao = await respostaRequisicao.Content.ReadAsStringAsync(cancellationToken);

            Assert.True(comando.Retorno.Status == respostaRequisicao.StatusCode,
                $"{mensagemErro}, " +
                $"Resposta {respostaRequisicao.StatusCode}, Esperado: {comando.Retorno.Status}");

            RemoverVariaveisDeAtribuicaoDoResultadoEDoResultadoPrevisto(
                resultadoPrevisto: ObterResultadoPreviso(comando.Retorno.Corpo?.ToString(),
                    respostaRequisicao.Content.Headers.ContentType?.MediaType),
                resultadoAtual: respostaTextoRequisicao,
                out var textoComVariavelRetorno, out var textoComValorRetorno);

            CompararRetornoJson(mensagemErro, textoComVariavelRetorno, textoComValorRetorno);
        }

        private static void RemoverVariaveisDeAtribuicaoDoResultadoEDoResultadoPrevisto(string resultadoPrevisto,
            string resultadoAtual, out string textoComVariavelRetorno, out string textoComValorRetorno) =>
            SubstituidorDeVariáveis.RemoverVariaveisDeAtribuicao(resultadoPrevisto, resultadoAtual,
                out textoComVariavelRetorno, out textoComValorRetorno);

        private static string ObterResultadoPreviso(string textoResultadoPrevisto, string mediaType)
        {
            if (string.IsNullOrEmpty(textoResultadoPrevisto))
                return string.Empty;
            if (textoResultadoPrevisto.StartsWith(SubstituidorDeVariáveis.IdentificadorAtribuidorVariávelSemAspasInicio))
                return textoResultadoPrevisto;
            if (mediaType == MediaTypeNames.Application.Json)
                return JToken.Parse(textoResultadoPrevisto).ToString(Formatting.None);
            return textoResultadoPrevisto;
        }

        private static HttpRequestMessage CriarHttpRequestMessageAPartirDoComando(Comando comando) =>
            new(new HttpMethod(comando.Requisição.Método), comando.Requisição.Rota)
            {
                Content = ObterConteúdoDaRequisição(comando)
            };

        private static HttpContent ObterConteúdoDaRequisição(Comando comando) =>
            comando.Requisição.Corpo is null
                ? default
                : new StringContent(((JToken)comando.Requisição.Corpo)?.ToString(Formatting.None), Encoding.UTF8,
                  MediaTypeNames.Application.Json);

        private static async Task<Cenário> ObtemCenário(string caminhoJsonTeste)
        {
            using var streamReader = new StreamReader(caminhoJsonTeste, Encoding.UTF8);
            var cenárioTexto = await streamReader.ReadToEndAsync();
            return JsonConvert.DeserializeObject<Cenário>(cenárioTexto);
        }

        private static void CompararRetornoJson(string mensagemErro, string textoComVariavelRetorno,
            string textoComValorRetorno)
        {
            var jsonComVariavelRetorno = JsonConvert.DeserializeObject<JToken>(textoComVariavelRetorno);
            var jsonComValorRetorno = JsonConvert.DeserializeObject<JToken>(textoComValorRetorno);

            if (!JToken.DeepEquals(jsonComVariavelRetorno, jsonComValorRetorno))
                Assert.True(textoComVariavelRetorno == textoComValorRetorno,
                    $"Mensagem: {mensagemErro}, Retorno: {textoComValorRetorno}");
        }
    }
}
