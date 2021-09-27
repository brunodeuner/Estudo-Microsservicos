using Estudo.Clientes.Serviço.Api;
using Estudo.Testes.Core.Api;
using System.Threading.Tasks;
using Xunit;

namespace Estudo.Testes.Clientes.DePontaAPonta
{
    public class TesteDeTodosOsCenários : IClassFixture<TestFixture<Startup>>
    {
        private readonly TestFixture<Startup> testFixture;

        public TesteDeTodosOsCenários(TestFixture<Startup> testFixture) => this.testFixture = testFixture;

        [Fact]
        public Task TestarTodosOsCenários() => ExecutarTodosOsCenários.Executar(testFixture);
    }
}
