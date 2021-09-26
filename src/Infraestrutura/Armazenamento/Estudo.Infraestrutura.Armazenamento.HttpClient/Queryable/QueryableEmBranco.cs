using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Estudo.Infraestrutura.Armazenamento.HttpClient.Queryable
{
    internal class QueryableEmBranco<T> : IQueryable<T>
    {
        public Type ElementType { get; }
        public Expression Expression { get; }
        public IQueryProvider Provider { get; }

        public IEnumerator<T> GetEnumerator() => default;

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => default;
    }
}
