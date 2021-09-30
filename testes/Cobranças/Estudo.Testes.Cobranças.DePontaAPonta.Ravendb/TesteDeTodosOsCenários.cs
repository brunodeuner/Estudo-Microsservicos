using Estudo.Aplicação.Configurações;
using Estudo.Cobranças.Aplicação.Armazenamento.Ravendb.MapReduces;
using Estudo.Cobranças.Domínio.Entidades;
using Estudo.Cobranças.Domínio.Repositórios;
using Estudo.Cobranças.Serviço.Api;
using Estudo.Core.Api.Testes;
using Estudo.Core.Infraestrutura.Armazenamento.Abstrações;
using Estudo.Core.Infraestrutura.Armazenamento.Ravendb;
using Estudo.Core.Infraestrutura.Armazenamento.Ravendb.Testes;
using Estudo.Core.Infraestrutura.Geral;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace Estudo.Cobranças.Testes.DePontaAPonta.Ravendb
{
    public class TesteDeTodosOsCenários : IClassFixture<WebHostFixture<Startup>>
    {
        private readonly WebHostFixture<Startup> testFixture;

        public TesteDeTodosOsCenários(WebHostFixture<Startup> testFixture) =>
            this.testFixture = testFixture;

        [Fact]
        public async Task TestarTodosOsCenários()
        {
            var configuraçãoDoRavendb = ObterConfiguraçãoDoRavendb();
            using var fabricaDoRavendb = new FabricaDoRavendb(configuraçãoDoRavendb);
            await fabricaDoRavendb.DocumentStore.ExecutarTarefaEmUmNovoBancoDeDados(configuraçãoDoRavendb, async () =>
            {
                await fabricaDoRavendb.DocumentStore.CriarMapReduces(default);

                await AdicionarCliente();

                var exceção = await Record.ExceptionAsync(() => testFixture.Executar());
                Assert.Null(exceção);
            });
        }

        private async Task AdicionarCliente()
        {
            var repositórioDePessoa = testFixture.ServiceProvider.GetRequiredService<RepositórioDePessoa>();
            await repositórioDePessoa.Salvar(new Pessoa("57251010020", "Rio Grande do Sul"), default);
            await repositórioDePessoa.Salvar(new Pessoa("27555728095", "Rio de Janeiro"), default);
            await testFixture.ServiceProvider.GetRequiredService<IDao>().SalvarAlterações(default);
        }

        private static ConfiguraçãoDoRavendb ObterConfiguraçãoDoRavendb()
        {
            var configuração = Configuração.CriarConfiguraçãoLendoOAppsettings();
            return configuração.GetSection(nameof(ConfiguraçãoDaConexão))
                .GetSection(nameof(ConfiguraçãoDoRavendb)).Get<ConfiguraçãoDoRavendb>();
        }
    }
}
