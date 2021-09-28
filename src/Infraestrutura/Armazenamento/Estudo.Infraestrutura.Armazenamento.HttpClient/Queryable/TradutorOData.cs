using System;
using System.Linq.Expressions;
using System.Text;

namespace Estudo.Infraestrutura.Armazenamento.HttpClient.Queryable
{
    public class TradutorOData : ExpressionVisitor
    {
        public int? Skip { get; private set; }
        public int? Take { get; private set; }

        public string ObterODataAPartirDaExpressao(Expression expression)
        {
            Visit(expression);
            return ObterOData();
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            switch (node.Method.Name)
            {
                case "Take":
                    if (ObterValorInteiroDaExpressao(node, out var valorTake))
                    {
                        Take = valorTake;
                        return Visit(node.Arguments[0]);
                    }
                    break;
                case "Skip":
                    if (ObterValorInteiroDaExpressao(node, out var valorSkip))
                    {
                        Skip = valorSkip;
                        return node;
                    }
                    break;
            }
            throw new NotSupportedException($"Método '{node.Method.Name}' não suportado");
        }

        private static bool ObterValorInteiroDaExpressao(MethodCallExpression expression, out int valor)
        {
            var expressãoComValorConstante = (ConstantExpression)expression.Arguments[1];
            if (expressãoComValorConstante.Value is int valorInteiro)
            {
                valor = valorInteiro;
                return true;
            }

            valor = default;
            return false;
        }

        private string ObterOData()
        {
            var resultado = new StringBuilder();

            if (Take.HasValue)
                AdicionarParametro(resultado, "$top=", Take.Value);

            if (Skip.HasValue)
                AdicionarParametro(resultado, "$skip=", Skip.Value);

            return resultado.ToString();
        }

        private static void AdicionarParametro(StringBuilder resultado, string nomeParametro, int valorParametro)
        {
            AdicionarNomeParametro(resultado, nomeParametro);
            resultado.Append(valorParametro);
        }

        private static void AdicionarNomeParametro(StringBuilder resultado, string nomeParametro)
        {
            if (resultado.Length != 0)
                resultado.Append('&');
            resultado.Append(nomeParametro);
        }
    }
}
