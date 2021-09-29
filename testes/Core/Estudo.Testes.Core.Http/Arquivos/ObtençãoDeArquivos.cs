using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Estudo.Core.Http.Testes.Arquivos
{
    internal static class ObtençãoDeArquivos
    {
        /// <summary>
        /// Obtêm arquivos ordenados independente de que nivel de pasta que ele se encontra.
        /// Ao em vez de escanear a pasta, obter os arquivos e ir descendo os niveis das pastas
        /// está função obtêm os arquivos descendo um nivel por vez.
        /// </summary>
        /// <param name="diretorioBusca">conforme Directory.EnumerateFiles(path)</param>
        /// <param name="searchPattern">conforme Directory.EnumerateFiles(searchPattern)</param>
        /// <returns></returns>
        public static IEnumerable<string> ObterArquivosOrdenados(string diretorioBusca,
            string searchPattern)
        {
            var arquivos = Directory.EnumerateFiles(diretorioBusca, searchPattern);
            var diretorios = Directory.GetDirectories(diretorioBusca);
            foreach (var diretorio in diretorios)
            {
                var arquivosDiretorio = ObterArquivosOrdenados(diretorio, searchPattern);
                if (arquivosDiretorio.Any())
                    arquivos = arquivos.Concat(arquivosDiretorio);
            }
            return arquivos;
        }
    }
}
