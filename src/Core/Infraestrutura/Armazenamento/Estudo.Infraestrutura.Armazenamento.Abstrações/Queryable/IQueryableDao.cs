using System.Linq;

namespace Estudo.Core.Infraestrutura.Armazenamento.Abstrações.Queryable
{
    public interface IQueryableDao<out T> : IOrderedQueryable<T>
    {
        IQueryable<T> QueryOriginal { get; }
    }
}
