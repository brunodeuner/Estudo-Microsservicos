using Estudo.Testes.Core.Http.Arquivos;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Estudo.Testes.Core.Http.Variáveis
{
    public static class Variáveis
    {
        private static readonly JsonSerializer jsonSerializer = new();
        private static readonly Dictionary<string, string> variaveisAtribuicao = new();

        public static bool ObterVariável(string nomeDaVariável, out string valor) =>
            variaveisAtribuicao.TryGetValue(nomeDaVariável, out valor);

        public static void AtribuirValorVariável(string nomeVariável, string valorVariável) =>
            variaveisAtribuicao.Add(nomeVariável, valorVariável);

        public static void AtribuirVariaveis(this object variável, string prefixo = default)
        {
            foreach (var propriedade in variável?.GetType()?.GetProperties()?.Where(x => x.CanRead &&
                !x.GetIndexParameters().Any()))
            {
                var valorPropriedade = propriedade.GetValue(variável);

                var nomeVariável = prefixo == default ? propriedade.Name : $"{prefixo}.{propriedade.Name}";

                AtribuirValorVariável(nomeVariável, valorPropriedade?.ToString());

                valorPropriedade.AtribuirVariaveis(nomeVariável);
            }
        }

        public static void AtribuirVariaveis<T>() => ObterVariaveis<T>().AtribuirVariaveis();

        public static T ObterVariaveis<T>()
        {
            var caminhoJsonVariaveis = Path.Combine(LocalAtualDeExecução.ObterLocalAtualDeExecução(),
                typeof(T).Name + ".json");
            using var streamReader = new StreamReader(caminhoJsonVariaveis, Encoding.UTF8);
            using var reader = new JsonTextReader(streamReader);
            return jsonSerializer.Deserialize<T>(reader);
        }

        public static void LimparVariável(string nomeVariável) => variaveisAtribuicao.Remove(nomeVariável);
    }
}
