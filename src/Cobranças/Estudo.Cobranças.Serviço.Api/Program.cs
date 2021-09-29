using Estudo.Serviço.Api;
using System.Threading.Tasks;

namespace Estudo.Cobranças.Serviço.Api
{
    internal static class Program
    {
        public static async Task Main(string[] args) => await CriarHostBuilder.CriarERodar<Startup>(args);
    }
}
