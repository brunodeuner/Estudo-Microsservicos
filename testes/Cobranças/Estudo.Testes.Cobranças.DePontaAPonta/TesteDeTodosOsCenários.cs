using Estudo.Cobran�as.Aplica��o.Armazenamento.Consumidores.Eventos;
using Estudo.Infraestrutura.Bus.Abstra��es.Produtor;
using Estudo.Testes.Core.Api;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace Estudo.Testes.Cobran�as.DePontaAPonta
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
            await ExecutarTodosOsCen�rios.Executar(testFixture);
        }

        private Task AdicionarCliente()
        {
            var produtor = testFixture.ServiceProvider.GetRequiredService<IProdutor>();
            return produtor.EnviarAsync(nameof(Cliente),
                new Infraestrutura.Bus.Abstra��es.Argumentos<Cliente>(new Cliente()
                {
                    Cpf = "57251010020"
                }), default);
        }
    }
}
