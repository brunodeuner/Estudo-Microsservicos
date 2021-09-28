using Estudo.Infraestrutura.Armazenamento.Ravendb.Testes;
using RichardSzalay.MockHttp;
using RichardSzalay.MockHttp.Matchers;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;
using Xunit;

namespace Estudo.Infraestrutura.Armazenamento.HttpClient.Testes
{
    public class LeituraDosDadosPeloIdNoDaoHttpClient
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
            await ObterDao(mockDeHttp.ToHttpClient()).Salvar(new EntidadeDeTeste()
            {
                Descrição = descriçãoDeTeste
            }, default);
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

        private static ValueTask<EntidadeDeTeste> ObterRegistro(System.Net.Http.HttpClient httpClient, string id)
        {
            var dao = ObterDao(httpClient);
            return dao.ObterPeloId<EntidadeDeTeste>(id, default);
        }

        private static Abstrações.IDao ObterDao(System.Net.Http.HttpClient httpClient)
        {
            var configuraçãoDoDaoHttpClient = new ConfiguraçãoDoDaoHttpClient()
            {
                ObterRotaAPartirDoTipo = tipo => urlDeTeste
            };

            return FabricaDeDaoHttpClient.ObterDao(httpClient, configuraçãoDoDaoHttpClient);
        }
    }
}
