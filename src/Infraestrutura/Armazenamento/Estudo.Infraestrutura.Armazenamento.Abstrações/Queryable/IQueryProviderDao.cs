using System.Linq;

namespace Estudo.Infraestrutura.Armazenamento.Abstrações.Queryable
{
    public interface IQueryProviderDao : IQueryProvider
    {
        IDao Dao { get; }
    }
}
