using System.Security.Cryptography.X509Certificates;

namespace Estudo.Core.Infraestrutura.Armazenamento.Ravendb
{
    public class ConfiguraçãoDoRavendb
    {
        public string[] UrlsConnection { get; set; }
        public string Database { get; set; }
        public string CaminhoDoCertificado { get; set; }

        public X509Certificate2 ObterCertificado() => new(CaminhoDoCertificado);
    }
}
