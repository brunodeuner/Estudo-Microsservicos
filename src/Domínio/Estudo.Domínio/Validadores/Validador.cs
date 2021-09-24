using System.Threading.Tasks;

namespace Estudo.Domínio.Validadores
{
    public class Validador : IValidador
    {
        public ValueTask<bool> Validar(object objetoAValidar)
        {
            return ValueTask.FromResult(true);
        }
    }
}
