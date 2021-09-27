using System.IO;
using System.Reflection;

namespace Estudo.Testes.Core.Http.Arquivos
{
    internal static class LocalAtualDeExecução
    {
        public static string ObterLocalAtualDeExecução() =>
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    }
}
