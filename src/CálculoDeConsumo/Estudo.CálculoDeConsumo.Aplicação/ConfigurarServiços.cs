using Estudo.Aplicação;
using Estudo.Aplicação.Configurações;
using Estudo.CálculoDeConsumo.Domínio.Entidades;
using Microsoft.Extensions.Configuration;
using System;

namespace Estudo.CálculoDeConsumo.Aplicação
{
    public static class ConfigurarServiços
    {
        public static void ConfigurarServiçosDaAplicação(this IConfiguration configuração)
        {
            var configuraçãoDaConexão = configuração.ObterConfiguracao<ConfiguraçãoDaConexão>();
            if (configuraçãoDaConexão?.ConfiguraçãoDoDaoHttpClient is null)
                return;

            configuraçãoDaConexão.ConfiguraçãoDoDaoHttpClient.ObterRotaAPartirDoTipo = tipo =>
            {
                if (tipo == typeof(Cobrança))
                    return new Uri("Clientes");

                if (tipo == typeof(Cobrança))
                    return new Uri("Cobranças");

                throw new ArgumentException($"{tipo} não configurado");
            };
        }
    }
}
