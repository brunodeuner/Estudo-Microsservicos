using Estudo.Aplica��o.Configura��es;
using Estudo.Cobran�as.Aplica��o.Armazenamento.Ravendb;
using Estudo.Cobran�as.Dom�nio.Entidades;
using Estudo.Cobran�as.Dom�nio.Reposit�rios;
using Estudo.Cobran�as.Servi�o.Api;
using Estudo.Infraestrutura.Armazenamento.Abstra��es;
using Estudo.Infraestrutura.Armazenamento.Ravendb;
using Estudo.Infraestrutura.Armazenamento.Ravendb.Testes;
using Estudo.Infraestrutura.Geral;
using Estudo.Testes.Core.Api;
using Microsoft.Extensions.Configuration;
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
            var configura��oDoRavendb = ObterConfigura��oDoRavendb();
            using var fabricaDoRavendb = new FabricaDoRavendb(configura��oDoRavendb);
            await fabricaDoRavendb.DocumentStore.ExecutarTarefaEmUmNovoBancoDeDados(configura��oDoRavendb, async () =>
            {
                await fabricaDoRavendb.DocumentStore.Inicializar(fabricaDoRavendb.Configura��oDoRavendb, default);

                await AdicionarCliente();

                var exce��o = await Record.ExceptionAsync(() => ExecutarTodosOsCen�rios.Executar(testFixture));
                Assert.Null(exce��o);
            });
        }

        private async Task AdicionarCliente()
        {
            var reposit�rioDePessoa = testFixture.ServiceProvider.GetRequiredService<Reposit�rioDePessoa>();
            await reposit�rioDePessoa.Salvar(new Pessoa("57251010020", "Rio Grande do Sul"), default);
            await reposit�rioDePessoa.Salvar(new Pessoa("27555728095", "Rio de Janeiro"), default);
            var dao = testFixture.ServiceProvider.GetRequiredService<IDao>();
            await dao.SalvarAltera��es(default);
        }

        private static Configura��oDoRavendb ObterConfigura��oDoRavendb()
        {
            var configura��o = Configura��o.CriarConfigura��oLendoOAppsettings();
            var configura��oDoRavendb = configura��o.GetSection(nameof(Configura��oDaConex�o))
                .GetSection(nameof(Configura��oDoRavendb)).Get<Configura��oDoRavendb>();
            return configura��oDoRavendb;
        }
    }
}
