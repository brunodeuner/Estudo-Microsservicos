using Estudo.Clientes.Serviço.Api;
using Estudo.Testes.Core.Api;

namespace Estudo.Testes.Clientes.DePontaAPonta
{
    public class TesteDeTodosOsCenários : TesteDeTodosOsCenários<Startup>
    {
        public TesteDeTodosOsCenários(WebHostFixture<Startup> testFixture) : base(testFixture)
        {
        }
    }
}
