using Estudo.CálculoDeConsumo.Domínio.Entidades;
using Estudo.CálculoDeConsumo.Domínio.Repositórios;
using Estudo.CálculoDeConsumo.Serviço.Api;
using Estudo.Infraestrutura.Armazenamento.Abstrações;
using Estudo.Testes.Core.Api;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Estudo.Testes.Consumo.DePontaAPonta
{
    public class TesteDeTodosOsCenários : IClassFixture<WebHostFixture<Startup>>
    {
        private const string CpfParaTeste = "97296984066";
        private readonly WebHostFixture<Startup> testFixture;

        public TesteDeTodosOsCenários(WebHostFixture<Startup> testFixture) => this.testFixture = testFixture;

        [Fact]
        public async Task CobrarTodosOsClientes_NenhumClienteCadastrado_NenhumaCobrançaAdicionada()
        {
            await ExecutarTodosOsCenários.Executar(testFixture);
            var dao = testFixture.ServiceProvider.GetRequiredService<IDao>();
            Assert.True(!dao.Selecionar<Cobrança>().Any());
        }

        [Fact]
        public async Task CobrarTodosOsClientes_ClienteExistente_CobrançaAdicionadaParaOCliente()
        {
            var serviceProvider = testFixture.ServiceProvider;
            await AdicionarNovoCLiente(serviceProvider);

            await ExecutarTodosOsCenários.Executar(testFixture);

            var cobrançaDoCpf = serviceProvider.GetRequiredService<IDao>().Selecionar<Cobrança>().Single();
            Assert.Equal(CpfParaTeste, cobrançaDoCpf.Cpf);
            Assert.Equal(DateTime.UtcNow.Date, cobrançaDoCpf.DataDeVencimento);
            Assert.Equal(9766, cobrançaDoCpf.Valor);
        }

        private static async Task AdicionarNovoCLiente(IServiceProvider serviceProvider)
        {
            var dao = serviceProvider.GetRequiredService<IDao>();
            var repositórioDeCliente = serviceProvider.GetRequiredService<RepositórioDeCliente>();
            await repositórioDeCliente.Salvar(new Cliente(CpfParaTeste), default);
            await dao.SalvarAlterações(default);
        }
    }
}
