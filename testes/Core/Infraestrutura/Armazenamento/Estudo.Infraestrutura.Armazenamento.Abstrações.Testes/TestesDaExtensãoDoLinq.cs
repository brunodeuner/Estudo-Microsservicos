using Estudo.Core.Infraestrutura.Armazenamento.Abstrações.Queryable;
using Estudo.Core.Infraestrutura.Armazenamento.Abstrações.Queryable.Leitura;
using Estudo.Core.Infraestrutura.Armazenamento.Abstrações.Testes.Dtos;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Estudo.Core.Infraestrutura.Armazenamento.Abstrações.Testes
{
    public class TestesDaExtensãoDoLinq
    {
        [Fact]
        public async Task ToAsyncEnumerable_QuerySemProviderDao_Exceção()
        {
            var query = new List<object>().AsQueryable();
            var exceção = await Assert.ThrowsAsync<NotImplementedException>(() => ProcessarRegistros(query));
            Assert.Equal("EnumerableQuery`1 não possui provider do tipo IQueryProviderDao", exceção.Message);
        }

        [Fact]
        public async Task ToAsyncEnumerable_DaoSemImplementarAInterface_Exceção()
        {
            var query = new List<object>().AsQueryable();
            var queryProvider = new QueryProviderDao(query.Provider, new Mock<IDao>().Object);
            var queryDao = new QueryableDao<object>(query, queryProvider);
            var exceção = await Assert.ThrowsAsync<NotImplementedException>(() => ProcessarRegistros(queryDao));
            Assert.Equal("IDaoProxy não implementa IToAsyncEnumerable", exceção.Message);
        }

        [Fact]
        public async Task ToAsyncEnumerable_QueryableQueNãoImplementaQueryableDao_Exceção()
        {
            var query = new List<object>().AsQueryable();
            var queryProvider = new QueryProviderDao(query.Provider, new Mock<IToAsyncEnumerable>().As<IDao>().Object);
            var queryDao = new QueryableEmbranco<object>(queryProvider);
            var exceção = await Assert.ThrowsAsync<NotImplementedException>(() => ProcessarRegistros(queryDao));
            Assert.Equal("QueryableEmbranco`1 não implementa IQueryableDao`1", exceção.Message);
        }

        private static async Task ProcessarRegistros(IQueryable<object> query)
        {
            var registrosProcessados = 0;
            await foreach (var registro in query.ToAsyncEnumerable(default))
                registrosProcessados++;
            Assert.Equal(0, registrosProcessados);
        }
    }
}
