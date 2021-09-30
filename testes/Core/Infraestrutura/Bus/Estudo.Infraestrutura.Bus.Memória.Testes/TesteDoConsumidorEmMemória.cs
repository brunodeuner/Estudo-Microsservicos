using Estudo.Core.Infraestrutura.Bus.Memória.Consumidor;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Estudo.Core.Infraestrutura.Bus.Memória.Testes
{
    public class TesteDoConsumidorEmMemória
    {
        [Fact]
        public async Task ConsumirEventos_TokenComCancelamento_ExceçãoDeOperaçãoCancelada()
        {
            var eventosPorTipo = new EventosPorTipo();
            var consumidorEmMemória = new ConsumidorEmMemória<object>(eventosPorTipo);
            var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(1)).Token;
            try
            {
                await consumidorEmMemória.Iniciar(string.Empty, cancellationToken);
            }
            catch (Exception e)
            {
                Assert.IsAssignableFrom<OperationCanceledException>(e);
            }
        }
    }
}
