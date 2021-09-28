using Estudo.Cobran�as.Dom�nio.Entidades;
using Estudo.Cobran�as.Dom�nio.Reposit�rios;
using Estudo.Cobran�as.Servi�o.Api;
using Estudo.Infraestrutura.Armazenamento.Abstra��es;
using Estudo.Testes.Core.Api;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace Estudo.Testes.Cobran�as.DePontaAPonta
{
    public class TesteDeTodosOsCen�rios : IClassFixture<WebHostFixture<Startup>>
    {
        private readonly WebHostFixture<Startup> testFixture;

        public TesteDeTodosOsCen�rios(WebHostFixture<Startup> testFixture) =>
            this.testFixture = testFixture;

        [Fact]
        public async Task TestarTodosOsCen�rios()
        {
            await AdicionarCliente();

            var exce��o = await Record.ExceptionAsync(() => ExecutarTodosOsCen�rios.Executar(testFixture));
            Assert.Null(exce��o);
        }

        private async Task AdicionarCliente()
        {
            var reposit�rioDePessoa = testFixture.ServiceProvider.GetRequiredService<Reposit�rioDePessoa>();
            await reposit�rioDePessoa.Salvar(new Pessoa("57251010020", "Rio Grande do Sul"), default);
            await reposit�rioDePessoa.Salvar(new Pessoa("27555728095", "Rio de Janeiro"), default);
            var dao = testFixture.ServiceProvider.GetRequiredService<IDao>();
            await dao.SalvarAltera��es(default);
        }
    }
}
