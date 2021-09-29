using Estudo.CálculoDeConsumo.Serviço.Api;
using Estudo.Core.Api.Testes;

namespace Estudo.CálculoDeConsumo.Testes.DePontaAPonta
{
    public class TesteDeTodosOsCenários : TesteDeTodosOsCenáriosComFixturePersonalizado<
        Startup, WebHostFixtureInjetandoMockDeHttpClient>
    {
        public TesteDeTodosOsCenários(WebHostFixtureInjetandoMockDeHttpClient testFixture) : base(testFixture) { }
    }
}
