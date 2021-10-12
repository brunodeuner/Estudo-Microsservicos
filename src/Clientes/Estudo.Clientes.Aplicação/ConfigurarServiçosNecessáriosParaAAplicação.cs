using Estudo.Clientes.Aplicação.Configurações;
using Estudo.Core.Infraestrutura.Bus.Abstrações.Produtor;
using Estudo.Core.Infraestrutura.Bus.RabbitMq.Produtor;
using Estudo.Core.Infraestrutura.Geral.Json.Abstrações;
using Estudo.Core.Infraestrutura.Geral.Json.SystemTextJson;
using Microsoft.Extensions.DependencyInjection;

namespace Estudo.Clientes.Aplicação
{
    public static class ConfigurarServiçosNecessáriosParaAAplicação
    {
        public static void ConfigurarServiçosDaAplicação(this IServiceCollection serviços,
            ConfiguraçãoDaAplicaçãoDeClientes configuraçãoDaAplicaçãoDeClientes)
        {
            serviços.AddSingleton(configuraçãoDaAplicaçãoDeClientes.ConfiguraçãoDaFila);
            serviços.AddTransient<ISerializador, Serializador>();
            serviços.AddTransient<IProdutor, ProdutorDoRabbitMq>();
        }
    }
}
