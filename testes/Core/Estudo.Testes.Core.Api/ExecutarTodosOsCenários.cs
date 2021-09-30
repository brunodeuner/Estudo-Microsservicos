using Estudo.Core.Http.Testes;
using System.Threading.Tasks;

namespace Estudo.Core.Api.Testes
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
