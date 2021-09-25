using System;

namespace Estudo.Infraestrutura.Bus.Abstrações.Consumidor.Dtos
{
    public class ArgumentosDoConsumo<T> : EventArgs
    {
        public ArgumentosDoConsumo(T corpo) => Corpo = corpo;

        public T Corpo { get; }
    }
}
