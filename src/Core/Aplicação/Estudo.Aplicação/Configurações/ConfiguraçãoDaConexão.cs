using Estudo.Core.Infraestrutura.Armazenamento.HttpClient;
using Estudo.Core.Infraestrutura.Armazenamento.Ravendb;
using System;

namespace Estudo.Core.Aplicação.Configurações
{
    public class ConfiguraçãoDaConexão
    {
        public string TipoDoDao { get; set; }
        public ConfiguraçãoDoRavendb ConfiguraçãoDoRavendb { get; set; }
        public ConfiguraçãoDoDaoHttpClient ConfiguraçãoDoDaoHttpClient { get; set; }
        public Type ObterTipo() => Type.GetType(TipoDoDao);
    }
}
