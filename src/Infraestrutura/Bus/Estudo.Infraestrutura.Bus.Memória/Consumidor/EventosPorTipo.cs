using Estudo.Infraestrutura.Bus.Abstrações;
using System.Collections.Concurrent;

namespace Estudo.Infraestrutura.Bus.Memória.Consumidor
{
    public class EventosPorTipo
    {
        private readonly ConcurrentDictionary<string, ConcurrentQueue<Argumentos<object>>> eventosPorTipo = new();

        public void AdicionarEvento<T>(string identificador, Argumentos<T> requisicao)
        {
            var eventosDoTipo = ObterOuCriarNovosEventosDoTipo(identificador);
            eventosDoTipo.Enqueue(new Argumentos<object>(requisicao.Corpo));
        }

        public Argumentos<T> ConsumirPróximoEvento<T>(string identificador)
        {
            var eventosDoTipo = ObterOuCriarNovosEventosDoTipo(identificador);
            eventosDoTipo.TryDequeue(out var evento);
            if (evento is null)
                return default;
            return new Argumentos<T>((T)evento.Corpo);
        }

        public void LimparEventos(string identificador)
        {
            if (eventosPorTipo.TryGetValue(identificador, out var eventosDoTipo))
                eventosDoTipo.Clear();
        }

        private ConcurrentQueue<Argumentos<object>> ObterOuCriarNovosEventosDoTipo(string identificador)
        {
            if (eventosPorTipo.TryGetValue(identificador, out var eventosDoTipo))
                return eventosDoTipo;

            eventosDoTipo = new ConcurrentQueue<Argumentos<object>>();
            return eventosPorTipo[identificador] = eventosDoTipo;
        }
    }
}
