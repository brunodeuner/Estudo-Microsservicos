using Estudo.Cobranças.Aplicação.Armazenamento.Consumidores.Eventos;
using Estudo.Core.Api.Testes;
using Estudo.Core.Infraestrutura.Bus.Abstrações;
using Estudo.Core.Infraestrutura.Bus.Abstrações.Produtor;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace Estudo.Cobranças.Testes.DePontaAPonta
{
    public class TesteDeTodosOsCenários : IClassFixture<WebHostFixtureInjetandoConsumidorEmMemória>
    {
        private readonly WebHostFixtureInjetandoConsumidorEmMemória testFixture;

        public TesteDeTodosOsCenários(WebHostFixtureInjetandoConsumidorEmMemória testFixture) =>
            this.testFixture = testFixture;

        [Fact]
        public async Task TestarTodosOsCenários()
        {
            await AdicionarCliente();
            var exceção = await Record.ExceptionAsync(() => testFixture.Executar());
            Assert.Null(exceção);
        }

        private Task AdicionarCliente()
        {
            var produtor = testFixture.ServiceProvider.GetRequiredService<IProdutor>();
            return produtor.EnviarAsync(nameof(Cliente), new EventoEventArgs<Cliente>(new Cliente()
            {
                Cpf = "57251010020",
                Estado = "Rio Grande do Sul"
            }), default);
        }
    }
}
