using Xunit;

namespace Estudo.Core.Infraestrutura.Armazenamento.Ravendb.Testes
{
    public class TestesDoDisposeDaFabricaDoRavendb
    {
        [Fact]
        public void FabricaDoRavendbDispose_DisposeSemNadaCriado_NenhumaExceção()
        {
            var fabricaDoRavendb = new FabricaDoRavendb(default);
            fabricaDoRavendb.Dispose();
            fabricaDoRavendb.Dispose();
            Assert.True(true);
        }
    }
}
