using Estudo.Infraestrutura.Bus.Abstrações;
using Estudo.Infraestrutura.Bus.Abstrações.Consumidor;
using Estudo.Infraestrutura.Bus.Abstrações.Consumidor.Dtos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Infraestrutura.Bus.Memória.Consumidor
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

        public event EventoAssíncrono<Argumentos<T>> Consumir;
        public event EventoAssíncrono<AgumentosDaExceção> Exceção;

        public async Task Iniciar(string identificador, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var evento = eventosPorTipo.ConsumirPróximoEvento<T>(identificador);
                if (evento is not null)
                {
                    try
                    {
                        await Consumir(new Argumentos<T>(evento.Corpo), cancellationToken);
                    }
                    catch (Exception e)
                    {
                        await Exceção(new AgumentosDaExceção(e), cancellationToken);
                    }
                    continue;
                }
                if (pararAoConsumirTodosOsEventos)
                    break;
                await Task.Delay(1, cancellationToken);
            }
        }
    }
}
