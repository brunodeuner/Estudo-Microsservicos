using Estudo.Clientes.Servi�o.Api;
using Estudo.Testes.Core.Api;
using System.Threading.Tasks;
using Xunit;

namespace Estudo.Testes.Clientes.DePontaAPonta
{
    public class TesteDeTodosOsCen�rios : IClassFixture<TestFixture<Startup>>
    {
        private readonly TestFixture<Startup> testFixture;

        public TesteDeTodosOsCen�rios(TestFixture<Startup> testFixture) => this.testFixture = testFixture;

        [Fact]
        public Task TestarTodosOsCen�rios() => ExecutarTodosOsCen�rios.Executar(testFixture);
    }
}
