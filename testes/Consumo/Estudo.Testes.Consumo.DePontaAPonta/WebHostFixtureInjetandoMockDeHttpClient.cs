using Estudo.CálculoDeConsumo.Serviço.Api;
using Estudo.Testes.Core.Api;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RichardSzalay.MockHttp;
using System;
using System.Net;
using System.Net.Http;

namespace Estudo.Testes.Cobranças.DePontaAPonta
{
    public class WebHostFixtureInjetandoMockDeHttpClient : WebHostFixture<Startup>
    {
        protected override void ConfigurarServiços(IConfiguration configuração, IServiceCollection serviços)
        {
            base.ConfigurarServiços(configuração, serviços);
            serviços.AddSingleton(() => CriarMockDeHttpClient());
        }

        private HttpClient CriarMockDeHttpClient()
        {
            var mockDeHttp = new MockHttpMessageHandler();

            mockDeHttp
                .When(HttpMethod.Get, new Uri(Cliente.BaseAddress, "Clientes").AbsoluteUri)
                .Respond(HttpStatusCode.OK)
                .WithContent("{\"Cpf\":\"97296984066\"}");

            mockDeHttp
                .When(HttpMethod.Post, new Uri(Cliente.BaseAddress, "Cobranças").AbsoluteUri)
                .Respond(HttpStatusCode.OK);

            return mockDeHttp.ToHttpClient();
        }
    }
}
