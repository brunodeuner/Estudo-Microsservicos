using Estudo.Infraestrutura.Bus.Memória.Consumidor;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Estudo.Infraestrutura.Bus.Memória.Testes
{
    public class TesteDeConsumidorEmMemória
    {
        [Fact]
        public async Task ConsumirEventos_TokenComCancelamento_ExceçãoDeOperaçãoCancelada()
        {
            var eventosPorTipo = new EventosPorTipo();
            var consumidorEmMemória = new ConsumidorEmMemória<object>(eventosPorTipo);
            var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(1)).Token;
            await Assert.ThrowsAsync<OperationCanceledException>(() =>
                consumidorEmMemória.Iniciar(string.Empty, cancellationToken));
        }
    }
}
