using Estudo.Cobranças.Domínio.Entidades;
using Estudo.Cobranças.Domínio.Repositórios;
using Estudo.Cobranças.Serviço.Api;
using Estudo.Infraestrutura.Armazenamento.Abstrações;
using Estudo.Testes.Core.Api;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace Estudo.Testes.Cobranças.DePontaAPonta
{
    public class TesteDeTodosOsCenários : IClassFixture<WebHostFixture<Startup>>
    {
        private readonly WebHostFixture<Startup> testFixture;

        public TesteDeTodosOsCenários(WebHostFixture<Startup> testFixture) =>
            this.testFixture = testFixture;

        [Fact]
        public async Task TestarTodosOsCenários()
        {
            await AdicionarCliente();

            var exceção = await Record.ExceptionAsync(() => ExecutarTodosOsCenários.Executar(testFixture));
            Assert.Null(exceção);
        }

        private async Task AdicionarCliente()
        {
            var repositórioDePessoa = testFixture.ServiceProvider.GetRequiredService<RepositórioDePessoa>();
            await repositórioDePessoa.Salvar(new Pessoa("57251010020", "Rio Grande do Sul"), default);
            await repositórioDePessoa.Salvar(new Pessoa("27555728095", "Rio de Janeiro"), default);
            var dao = testFixture.ServiceProvider.GetRequiredService<IDao>();
            await dao.SalvarAlterações(default);
        }
    }
}
