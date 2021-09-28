using System;
using System.Reflection;

namespace Estudo.Infraestrutura.Armazenamento.Abstrações
{
    public static class ExtensãoDeId
    {
        public static string ObterId(this object objeto)
        {
            var propriedadeComONomeId = objeto.ObterPropriedadeComONomeId();
            if (propriedadeComONomeId?.CanRead ?? false)
                return propriedadeComONomeId.GetValue(objeto)?.ToString();
            throw new ArgumentException($"{objeto.GetType().Name} não possui propriedade {nameof(IId.Id)} " +
                                        $"que possa ser lida");
        }

        public static string AtribuirNovoId(this object objeto)
        {
            var propriedadeComONomeId = objeto.ObterPropriedadeComONomeId();
            if (propriedadeComONomeId?.CanWrite ?? false)
            {
                var id = Guid.NewGuid().ToString();
                propriedadeComONomeId.SetValue(objeto, id);
                return id;
            }
            throw new ArgumentException($"{objeto.GetType().Name} não possui propriedade {nameof(IId.Id)} " +
                                        $"que possa ser atribuida");
        }

        private static PropertyInfo ObterPropriedadeComONomeId(this object objeto) =>
           objeto?.GetType().GetProperty(nameof(IId.Id));
    }
}
