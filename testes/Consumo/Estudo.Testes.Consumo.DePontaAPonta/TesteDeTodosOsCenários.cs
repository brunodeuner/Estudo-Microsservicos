using Estudo.C�lculoDeConsumo.Servi�o.Api;
using Estudo.Core.Api.Testes;

namespace Estudo.C�lculoDeConsumo.Testes.DePontaAPonta
{
    public class TesteDeTodosOsCen�rios : TesteDeTodosOsCen�riosComFixturePersonalizado<
        Startup, WebHostFixtureInjetandoMockDeHttpClient>
    {
        public TesteDeTodosOsCen�rios(WebHostFixtureInjetandoMockDeHttpClient testFixture) : base(testFixture) { }
    }
}
