using Xunit;

namespace Estudo.Infraestrutura.Armazenamento.Ravendb.Testes
{
    public class DisposeDoDaoEFabricaDoRavendb
    {
        [Fact]
        public void DaoRavendbDispose_DisposeSemNadaCriado_NenhumaExceção()
        {
            var daoRavendb = new DaoRavendb(default);
            daoRavendb.Dispose();
            daoRavendb.Dispose();
            Assert.True(true);
        }

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
