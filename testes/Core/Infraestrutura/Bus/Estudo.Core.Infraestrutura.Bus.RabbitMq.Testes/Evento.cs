using System;

namespace Estudo.Core.Infraestrutura.Bus.RabbitMq.Testes
{
    internal class Evento
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
