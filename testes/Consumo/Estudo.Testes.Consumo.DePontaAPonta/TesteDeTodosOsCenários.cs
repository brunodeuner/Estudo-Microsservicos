using Estudo.C�lculoDeConsumo.Dom�nio.Entidades;
using Estudo.Infraestrutura.Armazenamento.Abstra��es;
using Estudo.Testes.Cobran�as.DePontaAPonta;
using Estudo.Testes.Core.Api;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Estudo.Testes.Consumo.DePontaAPonta
{
    public class TesteDeTodosOsCen�rios : IClassFixture<WebHostFixtureInjetandoMockDeHttpClient>
    {
        private readonly WebHostFixtureInjetandoMockDeHttpClient testFixture;

        public TesteDeTodosOsCen�rios(WebHostFixtureInjetandoMockDeHttpClient testFixture) =>
            this.testFixture = testFixture;

        [Fact]
        public async Task CobrarTodosOsClientes_UmClienteCadastrado_Cobran�aAdicionadaParaOCliente()
        {
            var serviceProvider = testFixture.ServiceProvider;

            await ExecutarTodosOsCen�rios.Executar(testFixture);

            await foreach (var cobran�aDoCpf in serviceProvider.GetRequiredService<IDao>()
                .Selecionar<Cobran�a>().ToAsyncEnumerable(default))
            {
                Assert.Equal("97296984066", cobran�aDoCpf.Cpf);
                Assert.Equal(DateTime.UtcNow.Date, cobran�aDoCpf.DataDeVencimento);
                Assert.Equal(9766, cobran�aDoCpf.Valor);
            }
        }
    }
}
