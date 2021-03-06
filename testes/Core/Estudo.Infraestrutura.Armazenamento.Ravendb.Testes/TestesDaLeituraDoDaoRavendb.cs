using Estudo.Core.Infraestrutura.Armazenamento.Abstrações.Queryable;
using Estudo.Core.Infraestrutura.Armazenamento.Ravendb.Testes.Entidades;
using Estudo.Core.Infraestrutura.Armazenamento.Ravendb.Testes.Fabricas;
using Estudo.Core.Infraestrutura.Geral;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Xunit;

namespace Estudo.Core.Infraestrutura.Armazenamento.Ravendb.Testes
{
    public class TestesDaLeituraDoDaoRavendb
    {
        [Fact]
        public async Task ToAsyncEnumerable_SemNenhumRegistro_NenhumErro()
        {
            using var dao = FabricaDeDaoRavendb.ObterDao();
            var registrosLidos = 0;
            await foreach (var registro in dao.Selecionar<EntidadeComNenhumRegistro>().ToAsyncEnumerable(default))
                registrosLidos++;
            Assert.Equal(0, registrosLidos);
        }

        [Fact]
        public async Task ToAsyncEnumerable_ComRegistro_RegistroLido()
        {
            var configuração = Configuração.CriarConfiguraçãoLendoOAppsettings();
            var configuraçãoDoRavendb = configuração.GetSection(nameof(ConfiguraçãoDoRavendb))
                .Get<ConfiguraçãoDoRavendb>();
            configuraçãoDoRavendb.Database = "ToAsyncEnumerable_ComRegistro_RegistroLido";

            var fabricaDoRavendb = new FabricaDoRavendb(configuraçãoDoRavendb);
            await fabricaDoRavendb.DocumentStore.ExecutarTarefaEmUmNovoBancoDeDados(configuraçãoDoRavendb, async () =>
            {
                var entidade = new EntidadeDeTeste()
                {
                    Descrição = "Teste de descrição"
                };

                using var dao = new DaoRavendb(fabricaDoRavendb);
                await dao.Salvar(entidade, default);
                await dao.SalvarAlterações(default);

                var registrosLidos = 0;
                await foreach (var registro in dao.Selecionar<EntidadeDeTeste>().ToAsyncEnumerable(default))
                    registrosLidos++;
                Assert.Equal(1, registrosLidos);
            });
        }
    }
}
