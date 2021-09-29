namespace Estudo.Testes.Core.Api
{
    public class TesteDeTodosOsCenários<TStartup> : TesteDeTodosOsCenáriosComFixturePersonalizado<
        TStartup, WebHostFixture<TStartup>> where TStartup : class
    {
        public TesteDeTodosOsCenários(WebHostFixture<TStartup> testFixture) : base(testFixture) { }
    }
}
