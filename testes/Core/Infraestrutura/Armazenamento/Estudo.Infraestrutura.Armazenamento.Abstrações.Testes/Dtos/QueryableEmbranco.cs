using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Estudo.Core.Infraestrutura.Armazenamento.Abstrações.Testes.Dtos
{
    internal class QueryableEmbranco<T> : IOrderedQueryable<T>
    {
        public QueryableEmbranco(IQueryProvider provider) => Provider = provider;

        public Type ElementType => throw new NotImplementedException();

        public Expression Expression => throw new NotImplementedException();

        public IQueryProvider Provider { get; init; }

        public IEnumerator<T> GetEnumerator() => throw new NotImplementedException();

        IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();
    }
}
