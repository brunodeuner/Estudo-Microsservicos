using Estudo.Testes.Cobran�as.DePontaAPonta;
using Estudo.Testes.Core.Api;
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
            var exce��o = await Record.ExceptionAsync(() => ExecutarTodosOsCen�rios.Executar(testFixture));
            Assert.Null(exce��o);
        }
    }
}
