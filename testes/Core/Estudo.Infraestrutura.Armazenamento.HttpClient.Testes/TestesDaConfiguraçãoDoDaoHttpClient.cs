using System;
using System.Collections.Generic;
using Xunit;

namespace Estudo.Infraestrutura.Armazenamento.HttpClient.Testes
{
    public class TestesDaConfiguraçãoDoDaoHttpClient
    {
        [Fact]
        public void ObterRota_RotasPorTipoEObterRotaAPartirDoTipoConfigurados_RotaObtidaDaObterRotaAPartirDoTipo()
        {
            var configuração = new ConfiguraçãoDoDaoHttpClient()
            {
                RotasPorTipo = new Dictionary<string, Uri>()
                {
                    { "teste", new Uri("http://localhost/1") }
                },
                ObterRotaAPartirDoTipo = tipo => new Uri("http://localhost/2")
            };
            Assert.Equal("http://localhost/2", configuração.ObterRota(typeof(object)).AbsoluteUri);
        }

        [Fact]
        public void ObterRota_RotasPorTipoConfigurado_RotaObtidaDaRotasPorTipo()
        {
            var configuração = new ConfiguraçãoDoDaoHttpClient()
            {
                RotasPorTipo = new Dictionary<string, Uri>()
                {
                    { "System.Object", new Uri("http://localhost/1") }
                }
            };
            Assert.Equal("http://localhost/1", configuração.ObterRota(typeof(object)).AbsoluteUri);
        }
    }
}
