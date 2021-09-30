using Estudo.Aplicação;
using Estudo.Core.Serviço.Api.Filtros;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Estudo.Core.Serviço.Api
{
    public static class ConfigurarServiços
    {
        public static void ConfigurarServiçoEAplicação(this IServiceCollection serviços, IConfiguration configuração)
        {
            serviços.AddControllers(opções => opções.Filters.Add<FiltroDeNotificaçõesDoDomínio>());
            serviços.ConfigurarAplicação(configuração);
            serviços.AddSwaggerGen();
        }
    }
}
