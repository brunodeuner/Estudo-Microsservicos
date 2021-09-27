using Estudo.Testes.Core.Http;
using System.Threading.Tasks;

namespace Estudo.Testes.Core.Api
{
    public static class ExecutarTodosOsCenários
    {
        public static Task Executar<TStartup>(this WebHostFixture<TStartup> testFixture, string diretório = "Testes")
            where TStartup : class
        {
            testFixture.Iniciar();
            return ExecutorDeTestes.ExecutarTodosOsTestes(
                new ConfiguraçãoDosTestes(diretório, (_) => testFixture.Cliente));
        }
    }
}
