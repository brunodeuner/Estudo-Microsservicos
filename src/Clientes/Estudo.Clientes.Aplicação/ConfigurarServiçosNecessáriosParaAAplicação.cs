using Estudo.Clientes.Aplicação.Configurações;
using Estudo.Core.Infraestrutura.Bus.Abstrações.Produtor;
using Estudo.Core.Infraestrutura.Bus.RabbitMq.Produtor;
using Estudo.Core.Infraestrutura.Geral.Json.Abstrações;
using Estudo.Core.Infraestrutura.Geral.Json.SystemTextJson;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Estudo.Clientes.Aplicação
{
    public static class ConfigurarServiçosNecessáriosParaAAplicação
    {
        public static void ConfigurarServiçosDaAplicação(this IServiceCollection serviços, IConfiguration configuração)
        {
            var configuraçãoDaAplicaçãoDeClientes = configuração
               .GetSection(nameof(ConfiguraçãoDaAplicaçãoDeClientes))
               .Get<ConfiguraçãoDaAplicaçãoDeClientes>();
            serviços.AddSingleton(configuraçãoDaAplicaçãoDeClientes.ConfiguraçãoDaFila);
            serviços.AddTransient<ISerializador, Serializador>();
            serviços.AddTransient<IProdutor, ProdutorDoRabbitMq>();
        }
    }
}
