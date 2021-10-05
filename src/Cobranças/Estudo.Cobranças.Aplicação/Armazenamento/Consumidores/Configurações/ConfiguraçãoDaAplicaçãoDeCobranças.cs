using Estudo.Core.Infraestrutura.Bus.RabbitMq.Configurações;

namespace Estudo.Cobranças.Aplicação.Armazenamento.Consumidores.Configurações
{
    public class ConfiguraçãoDaAplicaçãoDeCobranças
    {
        public ConfiguraçãoDaFila ConfiguraçãoDaFila { get; set; }
        public bool InjetarConsumidorDoRabbitMq { get; set; }
    }
}
