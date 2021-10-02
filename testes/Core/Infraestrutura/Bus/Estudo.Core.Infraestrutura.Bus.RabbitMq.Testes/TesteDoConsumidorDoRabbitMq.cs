using Estudo.Core.Infraestrutura.Bus.RabbitMq.Configura��es;
using Estudo.Core.Infraestrutura.Bus.RabbitMq.Consumidor;
using Estudo.Core.Infraestrutura.Bus.RabbitMq.Produtor;
using Estudo.Core.Infraestrutura.Geral.Json.SystemTextJson;
using RabbitMQ.Client;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Estudo.Core.Infraestrutura.Bus.RabbitMq.Testes
{
    public class TesteDoConsumidorDoRabbitMq
    {
        [Fact]
        public async Task ConsumirEventos_TokenComCancelamento_Exce��oDeOpera��oCancelada()
        {
            var serializador = new Serializador();
            var configura��oDaFila = new Configura��oDaFila()
            {
                ConnectionFactory = new ConnectionFactory()
                {
                    DispatchConsumersAsync = true,
                    Uri = new Uri("amqps://tputowvs:9pYw6q83tkb8I8RzeExQWYAeIfUU22cB@jackal.rmq.cloudamqp.com/tputowvs")
                },
                Dur�vel = true
            };
            var produtor = new ProdutorDoRabbitMq(configura��oDaFila, serializador);
            var eventoEnviado = new Evento();
            await produtor.EnviarAsync(nameof(Evento), new Abstra��es.EventoEventArgs<Evento>(eventoEnviado), default);

            var deserializador = new Deserializador();
            var consumidor = new ConsumidorDoRabbitMq<Evento>(configura��oDaFila, deserializador);
            var quantidadeDeItensConsumidos = 0;
            consumidor.Consumir += (evento, cancellationToken) =>
            {
                quantidadeDeItensConsumidos++;
                Assert.Equal(eventoEnviado.Id, evento.Corpo.Id);
                return Task.CompletedTask;
            };
            await Assert.ThrowsAsync<TaskCanceledException>(() =>
                consumidor.Iniciar(nameof(Evento), new CancellationTokenSource(TimeSpan.FromSeconds(3)).Token));
            Assert.Equal(1, quantidadeDeItensConsumidos);
        }
    }
}
