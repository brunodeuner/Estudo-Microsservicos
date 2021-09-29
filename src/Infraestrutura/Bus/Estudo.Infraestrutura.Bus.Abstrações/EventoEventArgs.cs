using System;

namespace Estudo.Core.Infraestrutura.Bus.Abstrações
{
    public class EventoEventArgs<T> : EventArgs
    {
        public EventoEventArgs(T corpo) => Corpo = corpo;

        public T Corpo { get; }
    }
}
