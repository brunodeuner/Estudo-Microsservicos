using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Core.Domínio.Validadores
{
    public interface IValidador
    {
        ValueTask<bool> Validar<T>(T objetoAValidar, CancellationToken cancellationToken);
    }
}
