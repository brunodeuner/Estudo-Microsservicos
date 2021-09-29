using Estudo.Testes.Cobranças.DePontaAPonta;
using Estudo.Testes.Core.Api;
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
            var exceção = await Record.ExceptionAsync(() => ExecutarTodosOsCenários.Executar(testFixture));
            Assert.Null(exceção);
        }
    }
}
