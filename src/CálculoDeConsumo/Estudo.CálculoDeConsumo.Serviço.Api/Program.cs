using Estudo.Serviço.Api;
using System.Threading.Tasks;

namespace Estudo.CálculoDeConsumo.Serviço.Api
{
    public static class Program
    {
        public static async Task Main() => await CriarHostBuilder.CriarERodar<Startup>();
    }
}
