﻿using Estudo.Infraestrutura.Bus.Abstrações;
using Estudo.Infraestrutura.Bus.Abstrações.Consumidor;
using Estudo.Infraestrutura.Bus.Abstrações.Consumidor.Dtos;
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

        public event EventoAssíncrono<EventoEventArgs<T>> Consumir;

        public async Task Iniciar(string identificador, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
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
