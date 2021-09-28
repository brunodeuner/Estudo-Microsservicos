using System;
using System.Collections.Generic;

namespace Estudo.Infraestrutura.Armazenamento.HttpClient
{
    public class ConfiguraçãoDoDaoHttpClient
    {
        public IDictionary<string, Uri> RotasPorTipo { get; set; }
        public Func<Type, Uri> ObterRotaAPartirDoTipo { get; set; }
        public int QuantidadeDeRegistrosPorPaginação { get; set; } = 50;
        public bool ConfigurarHttpClientPadrão { get; set; }

        public Uri ObterRota(Type tipo) => ObterRotaAPartirDoTipo?.Invoke(tipo) ?? RotasPorTipo[tipo.FullName];
    }
}
