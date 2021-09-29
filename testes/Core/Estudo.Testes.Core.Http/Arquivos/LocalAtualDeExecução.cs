using System.IO;
using System.Reflection;

namespace Estudo.Core.Http.Testes.Arquivos
{
    internal static class LocalAtualDeExecução
    {
        public static string ObterLocalAtualDeExecução() =>
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    }
}
