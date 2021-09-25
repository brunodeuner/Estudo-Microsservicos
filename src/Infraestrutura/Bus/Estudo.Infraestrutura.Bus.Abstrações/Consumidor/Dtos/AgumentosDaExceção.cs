using System;

namespace Estudo.Infraestrutura.Bus.Abstrações.Consumidor.Dtos
{
    public class AgumentosDaExceção : EventArgs
    {
        public AgumentosDaExceção(Exception exception) => Exceção = exception;

        public Exception Exceção { get; }
    }
}
