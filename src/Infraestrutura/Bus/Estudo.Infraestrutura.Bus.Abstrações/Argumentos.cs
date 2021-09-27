using System;

namespace Estudo.Infraestrutura.Bus.Abstrações
{
    public class Argumentos<T> : EventArgs
    {
        public Argumentos(T corpo) => Corpo = corpo;

        public T Corpo { get; }
    }
}
