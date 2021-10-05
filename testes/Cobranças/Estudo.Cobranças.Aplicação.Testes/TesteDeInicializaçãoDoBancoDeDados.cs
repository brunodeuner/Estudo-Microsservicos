using Estudo.Cobranças.Aplicação.Armazenamento.Consumidores.Eventos;
using System.Threading.Tasks;
using Xunit;

namespace Estudo.Cobranças.Serviço.InicializaçãoDoBancoDeDados.Testes
{
    public class TesteDeInicializaçãoDoBancoDeDados
    {
        private const string NomeDaInscrição = nameof(Cliente);

        [Fact]
        public async Task Inicializar_RavendbVálido_SubscriptionCriado()
        {
            var exceção = await Record.ExceptionAsync(() => Program.Main());
            Assert.Null(exceção);
        }
    }
}
