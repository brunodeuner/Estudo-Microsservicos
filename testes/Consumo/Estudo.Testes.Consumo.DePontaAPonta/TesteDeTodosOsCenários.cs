using Estudo.CálculoDeConsumo.Serviço.Api;
using Estudo.Testes.Core.Api;

namespace Estudo.Testes.CálculoDeConsumo.DePontaAPonta
{
    public class TesteDeTodosOsCenários : TesteDeTodosOsCenáriosComFixturePersonalizado<
        Startup, WebHostFixtureInjetandoMockDeHttpClient>
    {
        public TesteDeTodosOsCenários(WebHostFixtureInjetandoMockDeHttpClient testFixture) : base(testFixture) { }
    }
}
