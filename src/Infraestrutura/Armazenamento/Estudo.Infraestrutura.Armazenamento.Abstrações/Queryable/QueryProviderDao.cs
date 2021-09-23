using System.Linq;
using System.Linq.Expressions;

namespace Estudo.Infraestrutura.Armazenamento.Abstrações.Queryable
{
    public class QueryProviderDao : IQueryProviderDao
    {
        public QueryProviderDao(IQueryProvider provider, IDao dao)
        {
            Provider = provider;
            Dao = dao;
        }

        public IQueryProvider Provider { get; }
        public IDao Dao { get; }

        public IQueryable CreateQuery(Expression expression) => Provider.CreateQuery(expression);
        public IQueryable<TElement> CreateQuery<TElement>(Expression expression) =>
            new QueryableDao<TElement>(Provider.CreateQuery<TElement>(expression), this);
        public object Execute(Expression expression) => Provider.Execute(expression);
        public TResult Execute<TResult>(Expression expression) => Provider.Execute<TResult>(expression);
    }
}
