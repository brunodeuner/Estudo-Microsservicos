using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Estudo.Infraestrutura.Armazenamento.HttpClient.Queryable
{
    public class TradutorOData : ExpressionVisitor
    {
        private const int ArgumentoComDoisValores = 2;
        private readonly StringBuilder condicaoWhere = new();

        public int? Skip { get; private set; }
        public int? Take { get; private set; }
        public string OrderBy { get; private set; }
        public string Where { get; private set; }

        public string ObterODataAPartirDaExpressao(Expression expression)
        {
            Visit(expression);
            ProcessarWhere();

            return ObterOData();
        }

        private void ProcessarWhere()
        {
            Where = condicaoWhere.ToString();
            condicaoWhere.Clear();
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            switch (node.Method.Name)
            {
                case "Where":
                    ProcessarMetodoWhere(node);
                    return node;
                case "FirstOrDefault":
                case "Any":
                    ProcessarMetodoWhere(node);
                    Take = 1;
                    return node;
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
                case "OrderBy":
                    if (ProcessarOrdenacao(node, "ASC"))
                        return Visit(node.Arguments[0]);
                    break;
                case "OrderByDescending":
                    if (ProcessarOrdenacao(node, "DESC"))
                        return Visit(node.Arguments[0]);
                    break;
            }
            throw new NotSupportedException(string.Format("The method '{0}' is not supported", node.Method.Name));
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Not:
                    condicaoWhere.Append(" NOT ");
                    Visit(node.Operand);
                    break;
                case ExpressionType.Convert:
                    Visit(node.Operand);
                    break;
            }
            return node;
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            AdicionarAndNaCondicaoWhereSeNecessario();

            condicaoWhere.Append('(');
            Visit(node.Left);

            switch (node.NodeType)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    condicaoWhere.Append(" and ");
                    break;
                case ExpressionType.Or:
                    condicaoWhere.Append(" or ");
                    break;
                case ExpressionType.Equal:
                    condicaoWhere.Append(IsNullConstant(node.Right) ? " IS " : " eq ");
                    break;
                case ExpressionType.NotEqual:
                    condicaoWhere.Append(IsNullConstant(node.Right) ? " IS NOT " : " ne ");
                    break;
                case ExpressionType.LessThan:
                    condicaoWhere.Append(" lt ");
                    break;
                case ExpressionType.LessThanOrEqual:
                    condicaoWhere.Append(" le ");
                    break;
                case ExpressionType.GreaterThan:
                    condicaoWhere.Append(" gt ");
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    condicaoWhere.Append(" ge ");
                    break;
                default:
                    throw new NotSupportedException(string.Format("The binary operator '{0}' is not supported",
                        node.NodeType));
            }

            Visit(node.Right);
            condicaoWhere.Append(')');
            return node;
        }

        private void AdicionarAndNaCondicaoWhereSeNecessario()
        {
            var condicaoWhereAtual = condicaoWhere.ToString().Replace("(", "").Replace(")", "").Replace(" ", "");
            if (condicaoWhereAtual.Length > 0
                && !condicaoWhereAtual.EndsWith("and")
                && !condicaoWhereAtual.EndsWith("or"))
                condicaoWhere.Append(" and ");
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (node.Value is IQueryable)
                return node;

            if (node.Value == null)
                condicaoWhere.Append("NULL");
            else
                AppendByValueType(node.Value);

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Expression.NodeType == ExpressionType.Parameter)
            {
                condicaoWhere.Append(node.Member.Name);
                return node;
            }

            if (node.Member is PropertyInfo propertyInfo)
            {
                var exp = (MemberExpression)node.Expression;
                var constant = (ConstantExpression)exp.Expression;
                var fieldInfoValue = ((FieldInfo)exp.Member).GetValue(constant.Value);
                var value = propertyInfo.GetValue(fieldInfoValue, null);
                condicaoWhere.Append(value);

                return node;
            }

            if (node.Member is FieldInfo fieldInfo && node.Expression is ConstantExpression constantExpression)
            {
                var value = fieldInfo.GetValue(constantExpression.Value);
                AppendByValueType(value);

                return node;
            }
            throw new NotSupportedException(string.Format("The member '{0}' is not supported", node.Member.Name));
        }

        private static bool IsNullConstant(Expression exp) =>
            exp.NodeType == ExpressionType.Constant && ((ConstantExpression)exp).Value == null;

        private void ProcessarMetodoWhere(MethodCallExpression node)
        {
            if (node.Arguments.Count != ArgumentoComDoisValores)
                return;

            Visit(node.Arguments[0]);
            var lambda = (LambdaExpression)ObterExpressaoLamdaSemAspas(node.Arguments[1]);
            Visit(lambda.Body);
        }

        private void AppendByValueType(object value)
        {
            switch (Type.GetTypeCode(value.GetType()))
            {
                case TypeCode.Boolean:
                    condicaoWhere.Append((bool)value ? 1 : 0);
                    break;
                case TypeCode.String:
                case TypeCode.DateTime:
                    condicaoWhere.Append('\'');
                    condicaoWhere.Append(value);
                    condicaoWhere.Append('\'');
                    break;
                case TypeCode.Object:
                    throw new NotSupportedException(string.Format("The constant for '{0}' is not supported", value));
                default:
                    condicaoWhere.Append(value);
                    break;
            }
        }

        private bool ProcessarOrdenacao(MethodCallExpression expression, string ordenacao)
        {
            var unary = (UnaryExpression)expression.Arguments[1];
            var lambdaExpression = (LambdaExpression)unary.Operand;

            if (lambdaExpression.Body is MemberExpression body)
            {
                if (!string.IsNullOrEmpty(OrderBy))
                    OrderBy += ", ";
                OrderBy += $"{body.Member.Name} {ordenacao}";
                return true;
            }

            return false;
        }

        private static bool ObterValorInteiroDaExpressao(MethodCallExpression expression, out int valor)
        {
            valor = default;

            var sizeExpression = (ConstantExpression)expression.Arguments[1];
            if (int.TryParse(sizeExpression.Value.ToString(), out var size))
            {
                valor = size;
                return true;
            }

            return false;
        }

        private string ObterOData()
        {
            var resultado = new StringBuilder();

            if (!string.IsNullOrEmpty(Where))
                AdicionarParametro(resultado, "$filter=", Where);

            if (Take.HasValue)
                AdicionarParametro(resultado, "$top=", Take.Value);

            if (Skip.HasValue)
                AdicionarParametro(resultado, "$skip=", Skip.Value);

            if (!string.IsNullOrEmpty(OrderBy))
                AdicionarParametro(resultado, "$orderby=", OrderBy);

            return resultado.ToString();
        }

        private static void AdicionarParametro(StringBuilder resultado, string nomeParametro, string valorParametro)
        {
            AdicionarNomeParametro(resultado, nomeParametro);
            resultado.Append(valorParametro);
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

        private static Expression ObterExpressaoLamdaSemAspas(Expression e)
        {
            while (e.NodeType == ExpressionType.Quote)
                e = ((UnaryExpression)e).Operand;

            return e;
        }
    }
}
