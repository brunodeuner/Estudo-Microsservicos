using Estudo.Aplicação;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Estudo.Serviço.Api
{
    public static class ConfigurarServiços
    {
        public static void ConfigurarServiçoEAplicação(this IServiceCollection serviços, IConfiguration configuração)
        {
            serviços.AddControllers(opções => opções.Filters.Add<FiltroDeNotificaçõesDoDomínio>());
            serviços.ConfigurarAplicação(configuração);
        }
    }
}
