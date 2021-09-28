using System;

namespace Estudo.Infraestrutura.Bus.Abstrações.Consumidor.Dtos
{
    public class ExceçãoEventArgs : EventArgs
    {
        public ExceçãoEventArgs(Exception exception) => Exceção = exception;

        public Exception Exceção { get; }
    }
}
