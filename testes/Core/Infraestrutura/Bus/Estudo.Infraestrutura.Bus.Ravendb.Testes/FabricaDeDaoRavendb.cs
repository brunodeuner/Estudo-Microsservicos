using Estudo.Infraestrutura.Armazenamento.Ravendb;
using Estudo.Infraestrutura.Geral;
using Microsoft.Extensions.Configuration;

namespace Estudo.Infraestrutura.Bus.Ravendb.Testes
{
    internal class FabricaDeDaoRavendb
    {
        public static DaoRavendb ObterDao()
        {
            var configuração = Configuração.CriarConfiguraçãoLendoOAppsettings();
            var configuraçãoDoRavendb = configuração.GetSection(nameof(ConfiguraçãoDoRavendb))
                .Get<ConfiguraçãoDoRavendb>();
            var fabricaDoRavendb = new FabricaDoRavendb(configuraçãoDoRavendb);
            return new DaoRavendb(fabricaDoRavendb);
        }
    }
}
