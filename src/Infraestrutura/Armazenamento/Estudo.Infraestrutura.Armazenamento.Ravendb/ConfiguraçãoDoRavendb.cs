using System;
using System.Security.Cryptography.X509Certificates;

namespace Estudo.Infraestrutura.Armazenamento.Ravendb
{
    public class ConfiguraçãoDoRavendb
    {
        public string[] UrlsConnection { get; set; }
        public string Database { get; set; }
        public string CaminhoDoCertificado { get; set; }
        public TimeSpan? TempoDeDuraçãoDoCache { get; set; }

        public X509Certificate2 ObterCertificado() => new X509Certificate2(CaminhoDoCertificado);
    }
}
