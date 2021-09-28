using Estudo.Infraestrutura.Geral;
using System;

namespace Estudo.Infraestrutura.Armazenamento.Abstrações
{
    public static class ExtensãoDeId
    {
        public static bool IdPreenchido(this object objeto) =>
            objeto.ObterId().Preenchido();

        public static string ObterId(this object objeto)
        {
            System.Reflection.PropertyInfo propriedadeComONomeId = ObterPropriedadeComONomeId(objeto);
            if (propriedadeComONomeId?.CanRead ?? false)
                return propriedadeComONomeId.GetValue(objeto)?.ToString();
            throw new ArgumentException($"{objeto.GetType().Name} não possui propriedade {nameof(IId.Id)} " +
                                        $"que possa ser lida");
        }

        public static string AtribuirNovoId(this object objeto)
        {
            var propriedadeComONomeId = objeto?.GetType().GetProperty(nameof(IId.Id));
            if (propriedadeComONomeId?.CanWrite ?? false)
            {
                var id = Guid.NewGuid().ToString();
                propriedadeComONomeId.SetValue(objeto, id);
                return id;
            }
            throw new ArgumentException($"{objeto.GetType().Name} não possui propriedade {nameof(IId.Id)} " +
                                        $"que possa ser atribuida");
        }

        private static System.Reflection.PropertyInfo ObterPropriedadeComONomeId(object objeto) =>
           objeto?.GetType().GetProperty(nameof(IId.Id));
    }
}
