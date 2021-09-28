using Estudo.CálculoDeConsumo.Domínio.Entidades;
using Estudo.Infraestrutura.Armazenamento.Abstrações;
using Estudo.Testes.Cobranças.DePontaAPonta;
using Estudo.Testes.Core.Api;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Estudo.Testes.Consumo.DePontaAPonta
{
    public class TesteDeTodosOsCenários : IClassFixture<WebHostFixtureInjetandoMockDeHttpClient>
    {
        private readonly WebHostFixtureInjetandoMockDeHttpClient testFixture;

        public TesteDeTodosOsCenários(WebHostFixtureInjetandoMockDeHttpClient testFixture) =>
            this.testFixture = testFixture;

        [Fact]
        public async Task CobrarTodosOsClientes_UmClienteCadastrado_CobrançaAdicionadaParaOCliente()
        {
            var serviceProvider = testFixture.ServiceProvider;

            await ExecutarTodosOsCenários.Executar(testFixture);

            await foreach (var cobrançaDoCpf in serviceProvider.GetRequiredService<IDao>()
                .Selecionar<Cobrança>().ToAsyncEnumerable(default))
            {
                Assert.Equal("97296984066", cobrançaDoCpf.Cpf);
                Assert.Equal(DateTime.UtcNow.Date, cobrançaDoCpf.DataDeVencimento);
                Assert.Equal(9766, cobrançaDoCpf.Valor);
            }
        }
    }
}
