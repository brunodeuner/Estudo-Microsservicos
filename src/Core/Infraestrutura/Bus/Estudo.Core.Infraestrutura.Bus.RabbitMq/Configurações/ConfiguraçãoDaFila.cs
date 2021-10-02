using RabbitMQ.Client;

namespace Estudo.Core.Infraestrutura.Bus.RabbitMq.Configurações
{
    public class ConfiguraçãoDaFila
    {
        public ConnectionFactory ConnectionFactory { get; set; }
        public bool Durável { get; set; }
        public bool Exclusiva { get; set; }
    }
}
