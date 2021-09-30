using Estudo.Core.Infraestrutura.Armazenamento.Abstrações.Queryable;
using Estudo.Core.Infraestrutura.Armazenamento.Memória;
using System.Threading.Tasks;
using Xunit;

namespace Estudo.Infraestrutura.Armazenamento.Memória.Testes
{
    public class TestesDoDaoMemória
    {
        [Fact]
        public async Task ToAsyncEnumerable_NenhumRegistro_NenhumRegistroRetornado()
        {
            var daoMemória = new DaoMemória();
            var registrosProcessados = 0;
            await foreach (var registro in daoMemória.Selecionar<object>().ToAsyncEnumerable(default))
                registrosProcessados++;
            Assert.Equal(0, registrosProcessados);
        }
    }
}
