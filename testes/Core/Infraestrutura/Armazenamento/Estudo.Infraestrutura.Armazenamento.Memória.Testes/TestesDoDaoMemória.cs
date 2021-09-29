using System.Linq;
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
