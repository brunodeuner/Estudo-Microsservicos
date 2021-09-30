namespace Estudo.Core.Api.Testes
{
    public class TesteDeTodosOsCenários<TStartup> : TesteDeTodosOsCenáriosComFixturePersonalizado<
        TStartup, WebHostFixture<TStartup>> where TStartup : class
    {
        public TesteDeTodosOsCenários(WebHostFixture<TStartup> testFixture) : base(testFixture) { }
    }
}
