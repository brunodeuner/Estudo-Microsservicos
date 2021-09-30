using System.Collections.Generic;

namespace Estudo.Core.Http.Testes.Variáveis
{
    public static class Variáveis
    {
        private static readonly Dictionary<string, string> variaveisAtribuicao = new();

        public static bool ObterVariável(string nomeDaVariável, out string valor) =>
            variaveisAtribuicao.TryGetValue(nomeDaVariável, out valor);

        public static void AtribuirValorVariável(string nomeVariável, string valorVariável) =>
            variaveisAtribuicao.Add(nomeVariável, valorVariável);
    }
}
