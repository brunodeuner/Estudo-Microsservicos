using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Domínio.Validadores
{
    public interface IValidador
    {
        ValueTask<bool> Validar<T>(T objetoAValidar, CancellationToken cancellationToken);
    }
}
