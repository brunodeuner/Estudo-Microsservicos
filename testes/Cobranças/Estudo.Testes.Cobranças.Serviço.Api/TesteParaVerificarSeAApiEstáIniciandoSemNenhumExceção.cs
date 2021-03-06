using Estudo.Core.Serviço.Api;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Estudo.Cobranças.Serviço.Api.Testes
{
    public class TesteParaVerificarSeAApiEstáIniciandoSemNenhumaExceção
    {
        [Fact]
        public async Task InicializaçãoDaApi_IniciarApi_ApiNãoDeuErroEmAtéUmSegundo()
        {
            var token = new CancellationTokenSource(TimeSpan.FromSeconds(1)).Token;
            await CriarHostBuilder.CriarERodar<Startup>(cancellationToken: token);
            Assert.True(token.IsCancellationRequested);
        }
    }
}
