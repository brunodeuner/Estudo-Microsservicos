using Estudo.Core.Infraestrutura.Armazenamento.Ravendb;
using Estudo.Core.Infraestrutura.Geral;
using Microsoft.Extensions.Configuration;

namespace Estudo.Core.Infraestrutura.Bus.Ravendb.Testes.Fabricas
{
    internal class FabricaDoDaoRavendb
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
