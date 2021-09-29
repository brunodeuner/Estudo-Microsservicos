using Estudo.Infraestrutura.Bus.Abstrações;
using System.Collections.Concurrent;

namespace Estudo.Infraestrutura.Bus.Memória.Consumidor
{
    public class EventosPorTipo
    {
        private readonly ConcurrentDictionary<string, ConcurrentQueue<EventoEventArgs<object>>> eventosPorTipo = new();

        public void AdicionarEvento<T>(string identificador, EventoEventArgs<T> requisicao)
        {
            var eventosDoTipo = ObterOuCriarNovosEventosDoTipo(identificador);
            eventosDoTipo.Enqueue(new EventoEventArgs<object>(requisicao.Corpo));
        }

        public EventoEventArgs<T> ConsumirPróximoEvento<T>(string identificador)
        {
            var eventosDoTipo = ObterOuCriarNovosEventosDoTipo(identificador);
            eventosDoTipo.TryDequeue(out var evento);
            if (evento is null)
                return default;
            return new EventoEventArgs<T>((T)evento.Corpo);
        }

        private ConcurrentQueue<EventoEventArgs<object>> ObterOuCriarNovosEventosDoTipo(string identificador)
        {
            if (eventosPorTipo.TryGetValue(identificador, out var eventosDoTipo))
                return eventosDoTipo;

            eventosDoTipo = new ConcurrentQueue<EventoEventArgs<object>>();
            return eventosPorTipo[identificador] = eventosDoTipo;
        }
    }
}
