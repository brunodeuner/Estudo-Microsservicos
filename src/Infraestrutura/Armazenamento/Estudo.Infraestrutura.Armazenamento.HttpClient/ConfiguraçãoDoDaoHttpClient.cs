using System;

namespace Estudo.Infraestrutura.Armazenamento.HttpClient
{
    public class ConfiguraçãoDoDaoHttpClient
    {
        public Func<Type, Uri> ObterRotaAPartirDoTipo { get; set; }
        public int QuantidadeDeRegistrosPorPaginação { get; set; } = 50;
        public bool ConfigurarHttpClientPadrão { get; set; }
    }
}
