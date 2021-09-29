using Estudo.Infraestrutura.Armazenamento.Ravendb.Testes.Entidades;
using Estudo.Infraestrutura.Armazenamento.Ravendb.Testes.Fabricas;
using Estudo.Infraestrutura.Geral;
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
                Descri��o = "Teste de descri��o"
            };

            await TestarAdicionarNaSess�o(entidade);

            using var dao = FabricaDeDaoRavendb.ObterDao();
            var entidadeLidaEmUmaNovaSess�o = await dao.ObterPeloId<EntidadeDeTeste>(entidade.Id, default);

            Assert.NotEqual(entidade, entidadeLidaEmUmaNovaSess�o);
            Assert.Equal(entidade.Id, entidadeLidaEmUmaNovaSess�o.Id);
            Assert.Equal(entidade.Descri��o, entidadeLidaEmUmaNovaSess�o.Descri��o);
        }

        [Fact]
        public async Task ObterPeloId_IdInexistente_RetornaDefault()
        {
            using var dao = FabricaDeDaoRavendb.ObterDao();
            var retorno = await dao.ObterPeloId<EntidadeDeTeste>("Id inexistente", default);
            Assert.Null(retorno);
        }

        private static async Task TestarAdicionarNaSess�o(EntidadeDeTeste entidade)
        {
            using var dao = FabricaDeDaoRavendb.ObterDao();
            await dao.Salvar(entidade, default);
            Assert.True(entidade.Id.Preenchido());

            var entidadeLidaNaSess�o = await dao.ObterPeloId<EntidadeDeTeste>(entidade.Id, default);
            Assert.Equal(entidade, entidadeLidaNaSess�o);

            await dao.SalvarAltera��es(default);
        }
    }
}
