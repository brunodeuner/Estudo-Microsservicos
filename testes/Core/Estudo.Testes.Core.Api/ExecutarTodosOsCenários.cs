using Estudo.Testes.Core.Http;
using System.Threading.Tasks;

namespace Estudo.Testes.Core.Api
{
    public static class ExecutarTodosOsCenários
    {
        public static Task Executar<TStartup>(this TestFixture<TStartup> testFixture) where TStartup : class =>
            ExecutorDeTestes.ExecutarTodosOsTestes(new ConfiguraçãoDosTestes("Testes", (_) => testFixture.Cliente));
    }
}
