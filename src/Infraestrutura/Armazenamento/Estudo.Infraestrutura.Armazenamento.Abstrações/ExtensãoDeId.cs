﻿namespace Estudo.Infraestrutura.Armazenamento.Abstrações
{
    public static class ExtensãoDeId
    {
        public static bool IdPreenchido(this object objeto) =>
            objeto.ObterId() is not null;

        public static string ObterId(this object objeto)
        {
            var propriedadeComONomeId = objeto?.GetType().GetProperty(nameof(IId.Id));
            return propriedadeComONomeId?.CanRead ?? false
                ? propriedadeComONomeId.GetValue(objeto)?.ToString()
                : default;
        }
    }
}
