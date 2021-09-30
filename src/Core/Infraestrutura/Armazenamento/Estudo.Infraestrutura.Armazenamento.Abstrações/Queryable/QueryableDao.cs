using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Estudo.Core.Infraestrutura.Armazenamento.Abstrações.Queryable
{
    public class QueryableDao<T> : IQueryableDao<T>
    {
        public QueryableDao(IQueryable<T> query, IQueryProvider provider)
        {
            QueryOriginal = query;
            Provider = provider;
        }

        public Type ElementType => QueryOriginal.ElementType;
        public Expression Expression => QueryOriginal.Expression;
        public IQueryProvider Provider { get; }
        public IQueryable<T> QueryOriginal { get; }

        public IEnumerator<T> GetEnumerator() => QueryOriginal.GetEnumerator();
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => QueryOriginal.GetEnumerator();
    }
}
