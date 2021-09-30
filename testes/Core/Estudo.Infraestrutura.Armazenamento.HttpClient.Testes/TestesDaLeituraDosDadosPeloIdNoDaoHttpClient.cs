using Estudo.Core.Infraestrutura.Armazenamento.Abstra��es;
using Estudo.Core.Infraestrutura.Armazenamento.Abstra��es.Queryable;
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
        private const string descri��oDeTeste = "Teste de descri��o";
        private static readonly Uri urlDeTeste = new("http://localhost/teste");

        [Fact]
        public async Task Adicionar_EntidadePreenchida_ExecutadoM�todoPostEConte�doDaRequisi��o�UmJsonDaEntidade()
        {
            var mockDeHttp = new MockHttpMessageHandler();
            mockDeHttp
                .When(HttpMethod.Post, urlDeTeste.AbsoluteUri)
                .Respond(HttpStatusCode.OK)
                .With(new MethodMatcher(HttpMethod.Post))
                .WithContent("{\"Id\":null,\"Descri��o\":\"" + descri��oDeTeste + "\"}");

            var exce��o = await Record.ExceptionAsync(async () => await ObterDao(mockDeHttp
                .ToHttpClient()).Salvar(new EntidadeDeTeste()
                {
                    Descri��o = descri��oDeTeste
                }, default));
            Assert.Null(exce��o);
        }

        [Fact]
        public async Task ObterPeloId_IdExistente_EntidadeCriadaRetornoDoHttpClient()
        {
            var mockDeHttp = new MockHttpMessageHandler();
            mockDeHttp
                .When(HttpMethod.Get, new Uri(urlDeTeste, idDeTeste).AbsoluteUri)
                .Respond(MediaTypeNames.Application.Json,
                "{\"Id\":\"" + idDeTeste + "\", \"Descri��o\":\"" + descri��oDeTeste + "\"}");

            var entidade = await ObterRegistro(mockDeHttp.ToHttpClient(), idDeTeste);
            Assert.NotNull(entidade);
            Assert.Equal(idDeTeste, entidade.Id);
            Assert.Equal(descri��oDeTeste, entidade.Descri��o);
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
                "[{\"Id\":\"0\", \"Descri��o\":\"" + descri��oDeTeste + "\"}]");

            mockDeHttp
                .When(HttpMethod.Get, urlDeTeste.AbsoluteUri)
                .WithQueryString("?$top=1&$skip=1")
                .Respond(MediaTypeNames.Application.Json,
                "[{\"Id\":\"1\", \"Descri��o\":\"" + descri��oDeTeste + "\"}]");

            mockDeHttp
                .When(HttpMethod.Get, urlDeTeste.AbsoluteUri)
                .WithQueryString("?$top=1&$skip=2")
                .Respond(MediaTypeNames.Application.Json, "[]");

            var dao = ObterDao(mockDeHttp.ToHttpClient());
            var quantidadeDeRegistrosLidos = 0;
            await foreach (var entidade in dao.Selecionar<EntidadeDeTeste>().ToAsyncEnumerable(default))
            {
                Assert.Equal(quantidadeDeRegistrosLidos.ToString(), entidade.Id);
                Assert.Equal(descri��oDeTeste, entidade.Descri��o);
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
            var configura��oDoDaoHttpClient = new Configura��oDoDaoHttpClient()
            {
                ObterRotaAPartirDoTipo = tipo => urlDeTeste,
                QuantidadeDeRegistrosPorPagina��o = 1
            };

            return FabricaDeDaoHttpClient.ObterDao(httpClient, configura��oDoDaoHttpClient);
        }
    }
}
