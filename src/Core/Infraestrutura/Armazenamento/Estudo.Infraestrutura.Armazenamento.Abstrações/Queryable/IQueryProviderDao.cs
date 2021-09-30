using System.Linq;

namespace Estudo.Core.Infraestrutura.Armazenamento.Abstrações.Queryable
{
    public interface IQueryProviderDao : IQueryProvider
    {
        IDao Dao { get; }
    }
}
