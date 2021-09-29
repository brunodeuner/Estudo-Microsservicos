using Estudo.Core.Infraestrutura.Armazenamento.Abstrações;
using Estudo.Core.Infraestrutura.Armazenamento.Abstrações.Queryable;
using RichardSzalay.MockHttp;
using RichardSzalay.MockHttp.Matchers;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;
using Xunit;

namespace Estudo.Core.Infraestrutura.Armazenamento.HttpClient.Testes
{
    public class TestesDaLeituraDosDadosPeloIdNoDaoHttpClient
    {
        private const string idDeTeste = nameof(idDeTeste);
        private const string descriçãoDeTeste = "Teste de descrição";
        private static readonly Uri urlDeTeste = new("http://localhost/teste");

        [Fact]
        public async Task Adicionar_EntidadePreenchida_ExecutadoMétodoPostEConteúdoDaRequisiçãoÉUmJsonDaEntidade()
        {
            var mockDeHttp = new MockHttpMessageHandler();
            mockDeHttp
                .When(HttpMethod.Post, urlDeTeste.AbsoluteUri)
                .Respond(HttpStatusCode.OK)
                .With(new MethodMatcher(HttpMethod.Post))
                .WithContent("{\"Id\":null,\"Descrição\":\"" + descriçãoDeTeste + "\"}");

            var exceção = await Record.ExceptionAsync(async () => await ObterDao(mockDeHttp
                .ToHttpClient()).Salvar(new EntidadeDeTeste()
                {
                    Descrição = descriçãoDeTeste
                }, default));
            Assert.Null(exceção);
        }

        [Fact]
        public async Task ObterPeloId_IdExistente_EntidadeCriadaRetornoDoHttpClient()
        {
            var mockDeHttp = new MockHttpMessageHandler();
            mockDeHttp
                .When(HttpMethod.Get, new Uri(urlDeTeste, idDeTeste).AbsoluteUri)
                .Respond(MediaTypeNames.Application.Json,
                "{\"Id\":\"" + idDeTeste + "\", \"Descrição\":\"" + descriçãoDeTeste + "\"}");

            var entidade = await ObterRegistro(mockDeHttp.ToHttpClient(), idDeTeste);
            Assert.NotNull(entidade);
            Assert.Equal(idDeTeste, entidade.Id);
            Assert.Equal(descriçãoDeTeste, entidade.Descrição);
        }

        [Fact]
        public async Task ObterPeloId_IdInexistente_RetornoComValorDefault()
        {
            var mockDeHttp = new MockHttpMessageHandler();
            mockDeHttp
                .When(HttpMethod.Get, new Uri(urlDeTeste, "IdInexistente").AbsoluteUri)
                .Respond(HttpStatusCode.NoContent);

            var entidade = await ObterRegistro(mockDeHttp.ToHttpClient(), "IdInexistente");
            Assert.Null(entidade);
        }

        [Fact]
        public async Task ToAsyncEnumerable_SemFiltro_RetornarPaginando()
        {
            var mockDeHttp = new MockHttpMessageHandler();
            mockDeHttp
                .When(HttpMethod.Get, urlDeTeste.AbsoluteUri)
                .WithQueryString("?$top=1&$skip=0")
                .Respond(MediaTypeNames.Application.Json,
                "[{\"Id\":\"0\", \"Descrição\":\"" + descriçãoDeTeste + "\"}]");

            mockDeHttp
                .When(HttpMethod.Get, urlDeTeste.AbsoluteUri)
                .WithQueryString("?$top=1&$skip=1")
                .Respond(MediaTypeNames.Application.Json,
                "[{\"Id\":\"1\", \"Descrição\":\"" + descriçãoDeTeste + "\"}]");

            mockDeHttp
                .When(HttpMethod.Get, urlDeTeste.AbsoluteUri)
                .WithQueryString("?$top=1&$skip=2")
                .Respond(MediaTypeNames.Application.Json, "[]");

            var dao = ObterDao(mockDeHttp.ToHttpClient());
            var quantidadeDeRegistrosLidos = 0;
            await foreach (var entidade in dao.Selecionar<EntidadeDeTeste>().ToAsyncEnumerable(default))
            {
                Assert.Equal(quantidadeDeRegistrosLidos.ToString(), entidade.Id);
                Assert.Equal(descriçãoDeTeste, entidade.Descrição);
                quantidadeDeRegistrosLidos++;
            }
            Assert.Equal(2, quantidadeDeRegistrosLidos);
        }

        private static ValueTask<EntidadeDeTeste> ObterRegistro(System.Net.Http.HttpClient httpClient, string id)
        {
            var dao = ObterDao(httpClient);
            return dao.ObterPeloId<EntidadeDeTeste>(id, default);
        }

        private static IDao ObterDao(System.Net.Http.HttpClient httpClient)
        {
            var configuraçãoDoDaoHttpClient = new ConfiguraçãoDoDaoHttpClient()
            {
                ObterRotaAPartirDoTipo = tipo => urlDeTeste,
                QuantidadeDeRegistrosPorPaginação = 1
            };

            return FabricaDeDaoHttpClient.ObterDao(httpClient, configuraçãoDoDaoHttpClient);
        }
    }
}
