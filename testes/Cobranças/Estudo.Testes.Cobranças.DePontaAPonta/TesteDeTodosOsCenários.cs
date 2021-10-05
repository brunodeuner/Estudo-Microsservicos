using Estudo.Cobranças.Aplicação.Armazenamento.Consumidores.Eventos;
using Estudo.Cobranças.Domínio.Repositórios;
using Estudo.Core.Api.Testes;
using Estudo.Core.Domínio.Eventos.Manutenção;
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
            await RemoverCliente();
            Assert.Null(await testFixture.ServiceProvider.GetRequiredService<RepositórioDePessoa>().ObterPeloId(
                ObterCliente().Id, default));
        }

        private Task AdicionarCliente()
        {
            var produtor = testFixture.ServiceProvider.GetRequiredService<IProdutor>();
            return produtor.EnviarAsync(nameof(EventoDeEntidadeSalva<Cliente>),
                new EventoEventArgs<EventoDeEntidadeSalva<Cliente>>(
                new EventoDeEntidadeSalva<Cliente>(ObterCliente())), default);
        }

        private Task RemoverCliente()
        {
            var produtor = testFixture.ServiceProvider.GetRequiredService<IProdutor>();
            return produtor.EnviarAsync(nameof(EventoDeEntidadeRemovida<Cliente>),
                new EventoEventArgs<EventoDeEntidadeRemovida<Cliente>>(
                new EventoDeEntidadeRemovida<Cliente>(ObterCliente())), default);
        }

        private static Cliente ObterCliente() => new()
        {
            Id = "57251010020/Pessoa",
            Cpf = "57251010020",
            Estado = "Rio Grande do Sul"
        };
    }
}
