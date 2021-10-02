using Estudo.Core.Infraestrutura.Bus.RabbitMq.Configurações;
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
        public async Task ConsumirEventos_TokenComCancelamento_ExceçãoDeOperaçãoCancelada()
        {
            var serializador = new Serializador();
            var configuraçãoDaFila = new ConfiguraçãoDaFila()
            {
                ConnectionFactory = new ConnectionFactory()
                {
                    DispatchConsumersAsync = true,
                    Uri = new Uri("amqps://tputowvs:9pYw6q83tkb8I8RzeExQWYAeIfUU22cB@jackal.rmq.cloudamqp.com/tputowvs")
                },
                Durável = true
            };
            var produtor = new ProdutorDoRabbitMq(configuraçãoDaFila, serializador);
            var eventoEnviado = new Evento();
            await produtor.EnviarAsync(nameof(Evento), new Abstrações.EventoEventArgs<Evento>(eventoEnviado), default);

            var deserializador = new Deserializador();
            var consumidor = new ConsumidorDoRabbitMq<Evento>(configuraçãoDaFila, deserializador);
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
