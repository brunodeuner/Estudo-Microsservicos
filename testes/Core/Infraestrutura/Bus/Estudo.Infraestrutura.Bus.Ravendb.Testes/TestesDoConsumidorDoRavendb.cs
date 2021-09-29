using Estudo.Infraestrutura.Armazenamento.Ravendb;
using Estudo.Infraestrutura.Armazenamento.Ravendb.Testes;
using Estudo.Infraestrutura.Bus.Ravendb.Consumidor;
using Estudo.Infraestrutura.Geral;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Xunit;

namespace Estudo.Infraestrutura.Bus.Ravendb.Testes
{
    public class TestesDoConsumidorDoRavendb
    {
        [Fact]
        public async Task Consumidor_ConsumirComSucesso_ConsumidorPara()
        {
            var configuraçãoDoRavendb = ObterConfiguraçãoDoRavendb();
            using var fabricaDoRavendb = new FabricaDoRavendb(configuraçãoDoRavendb);
            await fabricaDoRavendb.DocumentStore.ExecutarTarefaEmUmNovoBancoDeDados(configuraçãoDoRavendb, async () =>
                {
                    await CriarSubscriptionsNãoExistentes.CriarSubscriptionParaEntidadeDeTeste(
                        fabricaDoRavendb.DocumentStore, configuraçãoDoRavendb, default);
                    try
                    {
                        using var daoRavendb = new DaoRavendb(fabricaDoRavendb);
                        var entidadeDeTeste = await AdicionarEntidadeDeTeste(daoRavendb);

                        var consumidor = new ConsumidorDoRavendb<EntidadeDeTeste>(
                            fabricaDoRavendb.DocumentStore, configuraçãoDoRavendb, true);

                        var eventosConsumidos = 0;

                        consumidor.Consumir += (argumentos, cancellationToken) =>
                        {
                            Assert.Equal(entidadeDeTeste.Id, argumentos.Corpo.Id);
                            Assert.Equal(entidadeDeTeste.Descrição, argumentos.Corpo.Descrição);
                            eventosConsumidos++;
                            return Task.CompletedTask;
                        };
                        await consumidor.Iniciar(nameof(EntidadeDeTeste), default);

                        Assert.Equal(1, eventosConsumidos);
                    }
                    finally
                    {
                        await CriarSubscriptionsNãoExistentes.RemoverSubscriptionDeEntidadeDeTeste(
                            fabricaDoRavendb.DocumentStore, default);
                    }
                });
        }

        private static ConfiguraçãoDoRavendb ObterConfiguraçãoDoRavendb()
        {
            var configuração = Configuração.CriarConfiguraçãoLendoOAppsettings();
            var configuraçãoDoRavendb = configuração.GetSection(nameof(ConfiguraçãoDoRavendb))
                .Get<ConfiguraçãoDoRavendb>();
            return configuraçãoDoRavendb;
        }

        private static async Task<EntidadeDeTeste> AdicionarEntidadeDeTeste(DaoRavendb daoRavendb)
        {
            var entidadeDeTeste = new EntidadeDeTeste()
            {
                Descrição = "Teste",
            };
            await daoRavendb.Salvar(entidadeDeTeste, default);
            await daoRavendb.SalvarAlterações(default);
            return entidadeDeTeste;
        }
    }
}
