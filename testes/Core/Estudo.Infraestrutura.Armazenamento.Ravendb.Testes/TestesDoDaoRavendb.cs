using Estudo.Core.Infraestrutura.Geral;
using Estudo.Infraestrutura.Armazenamento.Ravendb.Testes.Entidades;
using Estudo.Infraestrutura.Armazenamento.Ravendb.Testes.Fabricas;
using System.Threading.Tasks;
using Xunit;

namespace Estudo.Infraestrutura.Armazenamento.Ravendb.Testes
{
    public class TestesDoDaoRavendb
    {
        [Fact]
        public async Task Adicionar_DefinidoObjeto_ObjetoAdicionado()
        {
            var entidade = new EntidadeDeTeste()
            {
                Descrição = "Teste de descrição"
            };

            await TestarAdicionarNaSessão(entidade);

            using var dao = FabricaDeDaoRavendb.ObterDao();
            var entidadeLidaEmUmaNovaSessão = await dao.ObterPeloId<EntidadeDeTeste>(entidade.Id, default);

            Assert.NotEqual(entidade, entidadeLidaEmUmaNovaSessão);
            Assert.Equal(entidade.Id, entidadeLidaEmUmaNovaSessão.Id);
            Assert.Equal(entidade.Descrição, entidadeLidaEmUmaNovaSessão.Descrição);
        }

        [Fact]
        public async Task ObterPeloId_IdInexistente_RetornaDefault()
        {
            using var dao = FabricaDeDaoRavendb.ObterDao();
            var retorno = await dao.ObterPeloId<EntidadeDeTeste>("Id inexistente", default);
            Assert.Null(retorno);
        }

        private static async Task TestarAdicionarNaSessão(EntidadeDeTeste entidade)
        {
            using var dao = FabricaDeDaoRavendb.ObterDao();
            await dao.Salvar(entidade, default);
            Assert.True(entidade.Id.Preenchido());

            var entidadeLidaNaSessão = await dao.ObterPeloId<EntidadeDeTeste>(entidade.Id, default);
            Assert.Equal(entidade, entidadeLidaNaSessão);

            await dao.SalvarAlterações(default);
        }
    }
}
