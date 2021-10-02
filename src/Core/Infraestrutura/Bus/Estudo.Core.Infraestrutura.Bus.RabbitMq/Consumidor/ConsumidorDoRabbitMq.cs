using Estudo.Core.Infraestrutura.Bus.Abstrações;
using Estudo.Core.Infraestrutura.Bus.Abstrações.Consumidor;
using Estudo.Core.Infraestrutura.Bus.Abstrações.Consumidor.Dtos;
using Estudo.Core.Infraestrutura.Bus.RabbitMq.Configurações;
using Estudo.Core.Infraestrutura.Geral.Json.Abstrações;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Core.Infraestrutura.Bus.RabbitMq.Consumidor
{
    public class ConsumidorDoRabbitMq<T> : IConsumidor<T> where T : class, new()
    {
        private readonly ConfiguraçãoDaFila configuraçãoDaFila;
        private readonly IDeserializador deserializador;

        public ConsumidorDoRabbitMq(ConfiguraçãoDaFila configuraçãoDaFila, IDeserializador deserializador)
        {
            this.configuraçãoDaFila = configuraçãoDaFila;
            this.deserializador = deserializador;
        }

        public event EventoAssíncrono<EventoEventArgs<T>> Consumir;

        public async Task Iniciar(string identificador, CancellationToken cancellationToken)
        {
            using var connection = configuraçãoDaFila.ConnectionFactory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.ConfigurarFila(identificador, configuraçãoDaFila);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += (sender, evento) => Consumir(new EventoEventArgs<T>(
                deserializador.Deserializar<T>(evento.Body.Span)), cancellationToken);

            channel.BasicConsume(queue: identificador,
                 autoAck: true,
                 consumer: consumer);

            await Task.Delay(Timeout.Infinite, cancellationToken);
        }
    }
}
