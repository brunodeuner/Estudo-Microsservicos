using Estudo.Infraestrutura.Armazenamento.Abstrações.Queryable;
using Estudo.Infraestrutura.Armazenamento.Abstrações.Queryable.Leitura;
using System.Collections.Generic;
using System.Threading;

namespace System.Linq
{
    public static class ExtensãoDoLinq
    {
        public static IAsyncEnumerable<T> ToAsyncEnumerable<T>(this IQueryable<T> query,
          CancellationToken cancellationToken) where T : class, new() =>
            ObterInterfaceAvancadaDoDao<T, IToAsyncEnumerable>(query)
            .ToAsyncEnumerable(ObterQueryOriginal(query), cancellationToken);

        private static Interface ObterInterfaceAvancadaDoDao<T, Interface>(IQueryable<T> query) where T : class, new()
        {
            if (query.Provider is IQueryProviderDao queryProviderDao)
            {
                if (queryProviderDao.Dao is Interface interfaceImplementada)
                    return interfaceImplementada;
                throw new NotImplementedException(
                    $"{queryProviderDao.Dao.GetType().Name} não implementa {typeof(Interface).Name}");
            }
            throw new NotImplementedException(
                $"{query.GetType().Name} não possui provider do tipo {typeof(IQueryProviderDao).Name}");
        }

        private static IQueryable<T> ObterQueryOriginal<T>(IQueryable<T> query) where T : class, new()
        {
            if (query is IQueryableDao<T> queryableDao)
                return queryableDao.QueryOriginal;
            throw new NotImplementedException(
                $"{query.GetType().Name} não implementa {typeof(IQueryableDao<T>).Name}");
        }
    }
}
