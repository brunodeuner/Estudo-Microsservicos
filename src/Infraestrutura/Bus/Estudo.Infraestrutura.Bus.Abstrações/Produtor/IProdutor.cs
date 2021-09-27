using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Infraestrutura.Bus.Abstrações.Produtor
{
    public interface IProdutor
    {
        Task EnviarAsync<T>(string identificador, Argumentos<T> requisicao, CancellationToken cancellationToken);
    }
}
