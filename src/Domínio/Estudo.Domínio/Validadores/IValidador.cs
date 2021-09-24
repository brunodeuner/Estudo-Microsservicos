using System.Threading.Tasks;

namespace Estudo.Domínio.Validadores
{
    public interface IValidador
    {
        ValueTask<bool> Validar(object objetoAValidar);
    }
}
