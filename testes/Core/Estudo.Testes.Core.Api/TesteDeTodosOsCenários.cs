using System.Threading.Tasks;
using Xunit;

namespace Estudo.Testes.Core.Api
{
    public class TesteDeTodosOsCenários<TStartup> : IClassFixture<WebHostFixture<TStartup>> where TStartup : class
    {
        private readonly WebHostFixture<TStartup> testFixture;

        public TesteDeTodosOsCenários(WebHostFixture<TStartup> testFixture) => this.testFixture = testFixture;

        [Fact]
        public Task TestarTodosOsCenários() => ExecutarTodosOsCenários.Executar(testFixture);
    }
}
