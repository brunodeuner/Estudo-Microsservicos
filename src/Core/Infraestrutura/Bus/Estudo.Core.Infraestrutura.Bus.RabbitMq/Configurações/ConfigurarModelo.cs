using RabbitMQ.Client;

namespace Estudo.Core.Infraestrutura.Bus.RabbitMq.Configurações
{
    internal static class ConfigurarModelo
    {
        public static void ConfigurarFila(this IModel modelo, string identificador,
            ConfiguraçãoDaFila configuraçãoDaFila) => modelo.QueueDeclare(
                queue: identificador,
                durable: configuraçãoDaFila.Durável,
                exclusive: configuraçãoDaFila.Exclusiva);
    }
}
