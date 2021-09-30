using Estudo.Clientes.Serviço.Api;
using Estudo.Core.Api.Testes;

namespace Estudo.Testes.Clientes.DePontaAPonta
{
    public class TesteDeTodosOsCenários : TesteDeTodosOsCenários<Startup>
    {
        public TesteDeTodosOsCenários(WebHostFixture<Startup> testFixture) : base(testFixture)
        {
        }
    }
}
