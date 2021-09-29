using Estudo.Aplicação.Configurações;
using Estudo.Cobranças.Aplicação.Armazenamento.Ravendb;
using Estudo.Cobranças.Domínio.Entidades;
using Estudo.Cobranças.Domínio.Repositórios;
using Estudo.Cobranças.Serviço.Api;
using Estudo.Infraestrutura.Armazenamento.Abstrações;
using Estudo.Infraestrutura.Armazenamento.Ravendb;
using Estudo.Infraestrutura.Armazenamento.Ravendb.Testes;
using Estudo.Infraestrutura.Geral;
using Estudo.Testes.Core.Api;
using Microsoft.Extensions.Configuration;
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
            var configuraçãoDoRavendb = ObterConfiguraçãoDoRavendb();
            using var fabricaDoRavendb = new FabricaDoRavendb(configuraçãoDoRavendb);
            await fabricaDoRavendb.DocumentStore.ExecutarTarefaEmUmNovoBancoDeDados(configuraçãoDoRavendb, async () =>
            {
                await fabricaDoRavendb.DocumentStore.Inicializar(fabricaDoRavendb.ConfiguraçãoDoRavendb, default);

                await AdicionarCliente();

                var exceção = await Record.ExceptionAsync(() => ExecutarTodosOsCenários.Executar(testFixture));
                Assert.Null(exceção);
            });
        }

        private async Task AdicionarCliente()
        {
            var repositórioDePessoa = testFixture.ServiceProvider.GetRequiredService<RepositórioDePessoa>();
            await repositórioDePessoa.Salvar(new Pessoa("57251010020", "Rio Grande do Sul"), default);
            await repositórioDePessoa.Salvar(new Pessoa("27555728095", "Rio de Janeiro"), default);
            var dao = testFixture.ServiceProvider.GetRequiredService<IDao>();
            await dao.SalvarAlterações(default);
        }

        private static ConfiguraçãoDoRavendb ObterConfiguraçãoDoRavendb()
        {
            var configuração = Configuração.CriarConfiguraçãoLendoOAppsettings();
            var configuraçãoDoRavendb = configuração.GetSection(nameof(ConfiguraçãoDaConexão))
                .GetSection(nameof(ConfiguraçãoDoRavendb)).Get<ConfiguraçãoDoRavendb>();
            return configuraçãoDoRavendb;
        }
    }
}
