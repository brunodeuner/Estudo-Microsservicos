using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Core.Infraestrutura.Bus.Abstrações.Produtor
{
    public interface IProdutor
    {
        Task EnviarAsync<T>(string identificador, EventoEventArgs<T> requisicao, CancellationToken cancellationToken);
    }
}
