using Estudo.CálculoDeConsumo.Serviço.Api;
using Estudo.Serviço.Api;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Estudo.Testes.Consumo.Serviço.Api
{
    public class TesteParaVerificarSeAApiEstáIniciandoSemNenhumExceção
    {
        [Fact]
        public async Task InicializaçãoDaApi_IniciarApi_ApiNãoDeuErroEmAtéCincoSegundos()
        {
            var token = new CancellationTokenSource(TimeSpan.FromSeconds(1)).Token;
            await CriarHostBuilder.CriarERodar<Startup>(cancellationToken: token);
            Assert.True(token.IsCancellationRequested);
        }
    }
}
