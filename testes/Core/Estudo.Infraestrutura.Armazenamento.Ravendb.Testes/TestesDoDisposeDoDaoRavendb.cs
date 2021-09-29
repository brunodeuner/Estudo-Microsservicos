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
    }
}
