using Xunit;

namespace Estudo.Core.Http.Testes.Variáveis
{
    internal static class SubstituidorDeVariáveis
    {
        public const string IdentificadorVariávelSemAspasInicio = "{{{";
        public const string IdentificadorVariávelSemAspasFinal = "}}}";
        public const string IdentificadorVariávelInicial = "{{";
        public const string IdentificadorVariávelFinal = "}}";
        public const string IdentificadorAtribuidorVariávelSemAspasInicio = "[[[";
        public const string IdentificadorAtribuidorVariávelSemAspasFinal = "]]]";
        public const string IdentificadorAtribuidorVariávelInicio = "[[";
        public const string IdentificadorAtribuidorVariávelFinal = "]]";

        public static string SubstituirVariável(this string texto) =>
            texto.SubstituirVariávelInterno(IdentificadorVariávelSemAspasInicio, IdentificadorVariávelSemAspasFinal,
                true).SubstituirVariávelInterno(IdentificadorVariávelInicial, IdentificadorVariávelFinal, false);

        private static string SubstituirVariávelInterno(this string texto, string textoInicialVariável, string textoFinalVariável,
            bool removerAspas)
        {
            var textoRetorno = texto;
            bool encontrouVariável;
            do
            {
                var nomeVariável = textoRetorno.ObterTextoEntre(textoInicialVariável, textoFinalVariável);
                encontrouVariável = !string.IsNullOrEmpty(nomeVariável);
                if (encontrouVariável)
                {
                    Assert.True(Variáveis.ObterVariável(nomeVariável, out var valorVariável), nomeVariável);

                    var indiceTextoInicialVariável = textoRetorno.IndexOf(textoInicialVariável);
                    var indiceTextoFinalVariável = textoRetorno.IndexOf(textoFinalVariável,
                        indiceTextoInicialVariável);
                    indiceTextoFinalVariável = indiceTextoFinalVariável == -1
                        ? textoRetorno.Length
                        : indiceTextoFinalVariável + textoFinalVariável.Length;

                    if (removerAspas)
                    {
                        indiceTextoInicialVariável--;
                        indiceTextoFinalVariável++;
                    }

                    var chaveVariável = textoRetorno[indiceTextoInicialVariável..indiceTextoFinalVariável];

                    textoRetorno = textoRetorno.Replace(chaveVariável, valorVariável);
                }
            } while (encontrouVariável);
            return textoRetorno;
        }

        public static void RemoverVariaveisDeAtribuicao(string textoComVariável, string texto2,
            out string textoComVariávelRetorno, out string textoComValorRetorno)
        {
            RemoverVariaveisDeAtribuicaoInterno(IdentificadorAtribuidorVariávelSemAspasInicio,
                IdentificadorAtribuidorVariávelSemAspasFinal, textoComVariável, texto2,
                out var textoComVariávelRetornoInterno, out var textoComValorRetornoInterno);
            RemoverVariaveisDeAtribuicaoInterno(IdentificadorAtribuidorVariávelInicio,
                IdentificadorAtribuidorVariávelFinal, textoComVariávelRetornoInterno, textoComValorRetornoInterno,
                out textoComVariávelRetorno, out textoComValorRetorno);
        }

        private static void RemoverVariaveisDeAtribuicaoInterno(string textoBuscaInicial, string textoBuscaFinal,
            string textoComVariável, string texto2, out string textoComVariávelRetorno,
            out string textoComValorRetorno)
        {
            textoComVariávelRetorno = textoComVariável;
            textoComValorRetorno = texto2;
            bool encontrouVariável;
            do
            {
                var nomeVariável = textoComVariávelRetorno.ObterTextoEntre(textoBuscaInicial, textoBuscaFinal);
                encontrouVariável = !string.IsNullOrEmpty(nomeVariável);
                if (encontrouVariável)
                {
                    var start = textoComVariávelRetorno.IndexOf(textoBuscaInicial);
                    Assert.True(textoComValorRetorno.Length >= start, $"{nameof(nomeVariável)}: {nomeVariável}, " +
                        $"{nameof(textoComValorRetorno)}: {textoComValorRetorno}");
                    var end = textoComValorRetorno.IndexOf('"', start);
                    end = end == -1 ? textoComValorRetorno.IndexOf(',', start) : end;
                    end = end == -1 ? textoComValorRetorno.Length : end;

                    var valorVariável = textoComValorRetorno[start..end];

                    Variáveis.AtribuirValorVariável(nomeVariável, valorVariável);

                    textoComVariávelRetorno = textoComVariávelRetorno.Replace(string.Concat(textoBuscaInicial,
                        nomeVariável, textoBuscaFinal), valorVariável);
                }
            } while (encontrouVariável);
        }

        public static string ObterTextoEntre(this string texto, string textoInicial, string textoFinal)
        {
            if (texto.Contains(textoInicial) && texto.Contains(textoFinal))
            {
                var posicaoInicial = texto.IndexOf(textoInicial) + textoInicial.Length;
                var posicaoFinal = texto.IndexOf(textoFinal, posicaoInicial);
                return texto[posicaoInicial..posicaoFinal];
            }
            return string.Empty;
        }
    }
}
