using Estudo.Core.Infraestrutura.Armazenamento.Ravendb;
using Estudo.Core.Infraestrutura.Geral;
using Microsoft.Extensions.Configuration;

namespace Estudo.Infraestrutura.Armazenamento.Ravendb.Testes.Fabricas
{
    internal class FabricaDeDaoRavendb
    {
        public static DaoRavendb ObterDao()
        {
            var configuração = Configuração.CriarConfiguraçãoLendoOAppsettings();
            var configuraçãoDoRavendb = configuração.GetSection(nameof(ConfiguraçãoDoRavendb)).Get<ConfiguraçãoDoRavendb>();
            var fabricaDoRavendb = new FabricaDoRavendb(configuraçãoDoRavendb);
            return new DaoRavendb(fabricaDoRavendb);
        }
    }
}
