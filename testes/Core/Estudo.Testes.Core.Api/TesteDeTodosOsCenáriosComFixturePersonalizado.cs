using System.Threading.Tasks;
using Xunit;

namespace Estudo.Testes.Core.Api
{
    public class TesteDeTodosOsCenáriosComFixturePersonalizado<TStartup, TFixture> : IClassFixture<TFixture>
        where TStartup : class
        where TFixture : WebHostFixture<TStartup>
    {
        private readonly WebHostFixture<TStartup> testFixture;

        public TesteDeTodosOsCenáriosComFixturePersonalizado(TFixture testFixture) => this.testFixture = testFixture;

        [Fact]
        public async Task TestarTodosOsCenários()
        {
            var exceção = await Record.ExceptionAsync(() => ExecutarTodosOsCenários.Executar(testFixture));
            Assert.Null(exceção);
        }
    }
}
