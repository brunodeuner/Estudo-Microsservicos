using Estudo.Core.Infraestrutura.Bus.Abstrações;
using Estudo.Core.Infraestrutura.Bus.Abstrações.Consumidor;
using Estudo.Core.Infraestrutura.Bus.Abstrações.Consumidor.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Core.Infraestrutura.Bus.Memória.Consumidor
{
    public class ConsumidorEmMemória<T> : IConsumidor<T> where T : class, new()
    {
        private readonly EventosPorTipo eventosPorTipo;
        private readonly bool pararAoConsumirTodosOsEventos;

        public ConsumidorEmMemória(EventosPorTipo eventosPorTipo, bool pararAoConsumirTodosOsEventos = false)
        {
            this.eventosPorTipo = eventosPorTipo;
            this.pararAoConsumirTodosOsEventos = pararAoConsumirTodosOsEventos;
        }

        public event EventoAssíncrono<EventoEventArgs<T>> Consumir;

        public async Task Iniciar(string identificador, CancellationToken cancellationToken)
        {
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var evento = eventosPorTipo.ConsumirPróximoEvento<T>(identificador);
                if (evento is not null)
                {
                    await Consumir(new EventoEventArgs<T>(evento.Corpo), cancellationToken);
                    continue;
                }
                if (pararAoConsumirTodosOsEventos)
                    break;
                await Task.Delay(1, cancellationToken);
            }
        }
    }
}
