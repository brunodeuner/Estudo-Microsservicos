using Estudo.Core.Infraestrutura.Armazenamento.Ravendb;

namespace Estudo.Cobranças.Aplicação.Armazenamento.Consumidores.Configurações
{
    public class ConfiguraçãoDaAplicaçãoDeCobranças
    {
        public ConfiguraçãoDoRavendb ConfiguraçãoDoRavendbParaOConsumidorDeClientes { get; set; }
        public bool InjetarConsumidorDoRavendb { get; set; }
    }
}
