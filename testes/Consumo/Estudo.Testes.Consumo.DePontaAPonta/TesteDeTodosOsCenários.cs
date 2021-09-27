using Estudo.C�lculoDeConsumo.Dom�nio.Entidades;
using Estudo.C�lculoDeConsumo.Dom�nio.Reposit�rios;
using Estudo.C�lculoDeConsumo.Servi�o.Api;
using Estudo.Infraestrutura.Armazenamento.Abstra��es;
using Estudo.Testes.Core.Api;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Estudo.Testes.Consumo.DePontaAPonta
{
    public class TesteDeTodosOsCen�rios : IClassFixture<WebHostFixture<Startup>>
    {
        private const string CpfParaTeste = "97296984066";
        private readonly WebHostFixture<Startup> testFixture;

        public TesteDeTodosOsCen�rios(WebHostFixture<Startup> testFixture) => this.testFixture = testFixture;

        [Fact]
        public async Task CobrarTodosOsClientes_NenhumClienteCadastrado_NenhumaCobran�aAdicionada()
        {
            await ExecutarTodosOsCen�rios.Executar(testFixture);
            var dao = testFixture.ServiceProvider.GetRequiredService<IDao>();
            Assert.True(!dao.Selecionar<Cobran�a>().Any());
        }

        [Fact]
        public async Task CobrarTodosOsClientes_ClienteExistente_Cobran�aAdicionadaParaOCliente()
        {
            var serviceProvider = testFixture.ServiceProvider;
            await AdicionarNovoCLiente(serviceProvider);

            await ExecutarTodosOsCen�rios.Executar(testFixture);

            var cobran�aDoCpf = serviceProvider.GetRequiredService<IDao>().Selecionar<Cobran�a>().Single();
            Assert.Equal(CpfParaTeste, cobran�aDoCpf.Cpf);
            Assert.Equal(DateTime.UtcNow.Date, cobran�aDoCpf.DataDeVencimento);
            Assert.Equal(9766, cobran�aDoCpf.Valor);
        }

        private static async Task AdicionarNovoCLiente(IServiceProvider serviceProvider)
        {
            var dao = serviceProvider.GetRequiredService<IDao>();
            var reposit�rioDeCliente = serviceProvider.GetRequiredService<Reposit�rioDeCliente>();
            await reposit�rioDeCliente.Salvar(new Cliente(CpfParaTeste), default);
            await dao.SalvarAltera��es(default);
        }
    }
}
