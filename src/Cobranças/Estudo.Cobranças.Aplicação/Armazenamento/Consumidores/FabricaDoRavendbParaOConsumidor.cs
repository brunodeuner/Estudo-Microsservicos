using Estudo.Core.Infraestrutura.Armazenamento.Ravendb;

namespace Estudo.Cobranças.Aplicação.Armazenamento.Consumidores
{
    internal class FabricaDoRavendbParaOConsumidor : FabricaDoRavendb
    {
        public FabricaDoRavendbParaOConsumidor(ConfiguraçãoDoRavendb configuraçãoDoRavendb) :
            base(configuraçãoDoRavendb)
        {
        }
    }
}
