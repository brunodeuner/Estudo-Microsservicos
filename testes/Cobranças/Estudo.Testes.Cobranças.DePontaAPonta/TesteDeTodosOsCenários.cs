using Estudo.Cobran�as.Aplica��o.Armazenamento.Consumidores.Eventos;
using Estudo.Core.Api.Testes;
using Estudo.Core.Infraestrutura.Bus.Abstra��es;
using Estudo.Core.Infraestrutura.Bus.Abstra��es.Produtor;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace Estudo.Cobran�as.Testes.DePontaAPonta
{
    public class TesteDeTodosOsCen�rios : IClassFixture<WebHostFixtureInjetandoConsumidorEmMem�ria>
    {
        private readonly WebHostFixtureInjetandoConsumidorEmMem�ria testFixture;

        public TesteDeTodosOsCen�rios(WebHostFixtureInjetandoConsumidorEmMem�ria testFixture) =>
            this.testFixture = testFixture;

        [Fact]
        public async Task TestarTodosOsCen�rios()
        {
            await AdicionarCliente();
            var exce��o = await Record.ExceptionAsync(() => testFixture.Executar());
            Assert.Null(exce��o);
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
