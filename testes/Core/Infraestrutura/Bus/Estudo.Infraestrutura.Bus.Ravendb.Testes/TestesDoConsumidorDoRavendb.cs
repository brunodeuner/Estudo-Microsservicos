using Estudo.Core.Infraestrutura.Armazenamento.Ravendb;
using Estudo.Core.Infraestrutura.Bus.Ravendb.Consumidor;
using Estudo.Core.Infraestrutura.Geral;
using Estudo.Infraestrutura.Armazenamento.Ravendb.Testes;
using Estudo.Infraestrutura.Bus.Ravendb.Testes.Entidades;
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
            var configura��oDoRavendb = ObterConfigura��oDoRavendb();
            using var fabricaDoRavendb = new FabricaDoRavendb(configura��oDoRavendb);
            await fabricaDoRavendb.DocumentStore.ExecutarTarefaEmUmNovoBancoDeDados(configura��oDoRavendb, async () =>
                {
                    await fabricaDoRavendb.DocumentStore.CriarSubscriptionParaEntidadeDeTeste(configura��oDoRavendb,
                        default);
                    try
                    {
                        using var daoRavendb = new DaoRavendb(fabricaDoRavendb);
                        var entidadeDeTeste = await AdicionarEntidadeDeTeste(daoRavendb);

                        var consumidor = new ConsumidorDoRavendb<EntidadeDeTeste>(
                            fabricaDoRavendb.DocumentStore, configura��oDoRavendb, true);

                        var eventosConsumidos = 0;

                        consumidor.Consumir += (argumentos, cancellationToken) =>
                        {
                            Assert.Equal(entidadeDeTeste.Id, argumentos.Corpo.Id);
                            Assert.Equal(entidadeDeTeste.Descri��o, argumentos.Corpo.Descri��o);
                            eventosConsumidos++;
                            return Task.CompletedTask;
                        };
                        await consumidor.Iniciar(nameof(EntidadeDeTeste), default);

                        Assert.Equal(1, eventosConsumidos);
                    }
                    finally
                    {
                        await fabricaDoRavendb.DocumentStore.RemoverSubscriptionDeEntidadeDeTeste(default);
                    }
                });
        }

        private static Configura��oDoRavendb ObterConfigura��oDoRavendb()
        {
            var configura��o = Configura��o.CriarConfigura��oLendoOAppsettings();
            var configura��oDoRavendb = configura��o.GetSection(nameof(Configura��oDoRavendb))
                .Get<Configura��oDoRavendb>();
            return configura��oDoRavendb;
        }

        private static async Task<EntidadeDeTeste> AdicionarEntidadeDeTeste(DaoRavendb daoRavendb)
        {
            var entidadeDeTeste = new EntidadeDeTeste()
            {
                Descri��o = "Teste",
            };
            await daoRavendb.Salvar(entidadeDeTeste, default);
            await daoRavendb.SalvarAltera��es(default);
            return entidadeDeTeste;
        }
    }
}
