using Estudo.Core.Infraestrutura.Bus.Abstrações;
using Estudo.Core.Infraestrutura.Bus.Abstrações.Produtor;
using Estudo.Core.Infraestrutura.Bus.RabbitMq.Configurações;
using Estudo.Core.Infraestrutura.Geral.Json.Abstrações;
using RabbitMQ.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Core.Infraestrutura.Bus.RabbitMq.Produtor
{
    public class ProdutorDoRabbitMq : IProdutor
    {
        private readonly ConfiguraçãoDaFila configuraçãoDaFila;
        private readonly ISerializador serializador;

        public ProdutorDoRabbitMq(ConfiguraçãoDaFila configuraçãoDaFila, ISerializador serializador)
        {
            this.configuraçãoDaFila = configuraçãoDaFila;
            this.serializador = serializador;
        }

        public Task EnviarAsync<T>(string identificador, EventoEventArgs<T> requisicao,
            CancellationToken cancellationToken)
        {
            using var connection = configuraçãoDaFila.ConnectionFactory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.ConfigurarFila(identificador, configuraçãoDaFila);

            var corpo = serializador.Serializar(requisicao.Corpo);

            channel.BasicPublish(exchange: string.Empty,
                                 routingKey: identificador,
                                 body: new ReadOnlyMemory<byte>(corpo));

            return Task.CompletedTask;
        }
    }
}
