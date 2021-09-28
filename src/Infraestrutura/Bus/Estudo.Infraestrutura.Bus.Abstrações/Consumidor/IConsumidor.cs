using Estudo.Infraestrutura.Bus.Abstrações.Consumidor.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Infraestrutura.Bus.Abstrações.Consumidor
{
    public interface IConsumidor<T>
    {
        event EventoAssíncrono<EventoEventArgs<T>> Consumir;
        Task Iniciar(string identificador, CancellationToken cancellationToken);
    }
}
