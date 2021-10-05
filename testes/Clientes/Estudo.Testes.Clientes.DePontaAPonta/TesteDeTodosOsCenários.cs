using Estudo.Clientes.Serviço.Api;
using Estudo.Core.Api.Testes;

namespace Estudo.Clientes.Testes.DePontaAPonta
{
    public class TesteDeTodosOsCenários : TesteDeTodosOsCenários<Startup>
    {
        public TesteDeTodosOsCenários(WebHostFixture<Startup> testFixture) : base(testFixture) { }
    }
}
