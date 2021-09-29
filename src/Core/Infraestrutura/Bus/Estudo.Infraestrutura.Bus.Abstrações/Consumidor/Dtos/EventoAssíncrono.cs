using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Core.Infraestrutura.Bus.Abstrações.Consumidor.Dtos
{
    public delegate Task EventoAssíncrono<in T>(T evento, CancellationToken cancellationToken);
}
