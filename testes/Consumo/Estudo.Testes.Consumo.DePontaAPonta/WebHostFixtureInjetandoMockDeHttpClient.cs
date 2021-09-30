using Estudo.CálculoDeConsumo.Serviço.Api;
using Estudo.Core.Api.Testes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RichardSzalay.MockHttp;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Mime;

namespace Estudo.CálculoDeConsumo.Testes.DePontaAPonta
{
    public class WebHostFixtureInjetandoMockDeHttpClient : WebHostFixture<Startup>
    {
        protected override void ConfigurarServiços(IConfiguration configuração, IServiceCollection serviços)
        {
            base.ConfigurarServiços(configuração, serviços);
            serviços.AddSingleton(CriarMockDeHttpClient(new Uri("http://localhost")));
        }

        private static HttpClient CriarMockDeHttpClient(Uri uriBase)
        {
            var mockDeHttp = new MockHttpMessageHandler();

            mockDeHttp
                .When(HttpMethod.Get, new Uri(uriBase, "Clientes").AbsoluteUri)
                .WithQueryString("?$top=50&$skip=0")
                .Respond(MediaTypeNames.Application.Json, "[{\"Cpf\":\"97296984066\"}]");

            mockDeHttp
                .When(HttpMethod.Post, new Uri(uriBase, "Cobranças").AbsoluteUri)
                .Respond(HttpStatusCode.OK);

            return mockDeHttp.ToHttpClient();
        }
    }
}
