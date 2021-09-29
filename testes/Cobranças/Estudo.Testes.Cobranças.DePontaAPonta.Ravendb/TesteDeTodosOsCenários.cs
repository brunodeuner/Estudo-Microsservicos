using Estudo.Aplica��o.Configura��es;
using Estudo.Cobran�as.Aplica��o.Armazenamento.Ravendb.MapReduces;
using Estudo.Cobran�as.Dom�nio.Entidades;
using Estudo.Cobran�as.Dom�nio.Reposit�rios;
using Estudo.Cobran�as.Servi�o.Api;
using Estudo.Core.Api.Testes;
using Estudo.Core.Infraestrutura.Armazenamento.Abstra��es;
using Estudo.Core.Infraestrutura.Armazenamento.Ravendb;
using Estudo.Core.Infraestrutura.Armazenamento.Ravendb.Testes;
using Estudo.Core.Infraestrutura.Geral;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace Estudo.Cobran�as.Testes.DePontaAPonta.Ravendb
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
                await fabricaDoRavendb.DocumentStore.CriarMapReduces(default);

                await AdicionarCliente();

                var exce��o = await Record.ExceptionAsync(() => testFixture.Executar());
                Assert.Null(exce��o);
            });
        }

        private async Task AdicionarCliente()
        {
            var reposit�rioDePessoa = testFixture.ServiceProvider.GetRequiredService<Reposit�rioDePessoa>();
            await reposit�rioDePessoa.Salvar(new Pessoa("57251010020", "Rio Grande do Sul"), default);
            await reposit�rioDePessoa.Salvar(new Pessoa("27555728095", "Rio de Janeiro"), default);
            await testFixture.ServiceProvider.GetRequiredService<IDao>().SalvarAltera��es(default);
        }

        private static Configura��oDoRavendb ObterConfigura��oDoRavendb()
        {
            var configura��o = Configura��o.CriarConfigura��oLendoOAppsettings();
            return configura��o.GetSection(nameof(Configura��oDaConex�o))
                .GetSection(nameof(Configura��oDoRavendb)).Get<Configura��oDoRavendb>();
        }
    }
}
