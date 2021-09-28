using Estudo.Cobranças.Aplicação.Armazenamento.Consumidores.Eventos;
using Estudo.Infraestrutura.Bus.Abstrações.Produtor;
using Estudo.Testes.Core.Api;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace Estudo.Testes.Cobranças.DePontaAPonta
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

            var exceção = await Record.ExceptionAsync(() => ExecutarTodosOsCenários.Executar(testFixture));
            Assert.Null(exceção);
        }

        private Task AdicionarCliente()
        {
            var produtor = testFixture.ServiceProvider.GetRequiredService<IProdutor>();
            return produtor.EnviarAsync(nameof(Cliente),
                new Infraestrutura.Bus.Abstrações.EventoEventArgs<Cliente>(new Cliente()
                {
                    Cpf = "57251010020"
                }), default);
        }
    }
}
