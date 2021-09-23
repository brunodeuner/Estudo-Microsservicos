using System.Security.Cryptography.X509Certificates;
using static Raven.Client.Documents.Conventions.DocumentConventions;

namespace Estudo.Infraestrutura.Armazenamento.Ravendb
{
    public class Configuração
    {
        public string[] UrlsConnection { get; set; }
        public string Database { get; set; }
        public X509Certificate2 Certificado { get; set; }
        public AggressiveCacheConventions CacheAgressivo { get; set; }
    }
}
