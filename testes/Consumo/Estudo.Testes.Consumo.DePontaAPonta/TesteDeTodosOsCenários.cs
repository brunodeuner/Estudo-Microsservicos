using Estudo.C�lculoDeConsumo.Servi�o.Api;
using Estudo.Testes.Core.Api;

namespace Estudo.Testes.C�lculoDeConsumo.DePontaAPonta
{
    public class TesteDeTodosOsCen�rios : TesteDeTodosOsCen�riosComFixturePersonalizado<
        Startup, WebHostFixtureInjetandoMockDeHttpClient>
    {
        public TesteDeTodosOsCen�rios(WebHostFixtureInjetandoMockDeHttpClient testFixture) : base(testFixture) { }
    }
}
