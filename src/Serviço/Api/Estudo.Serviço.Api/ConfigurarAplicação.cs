using Estudo.Serviço.Api.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Estudo.Serviço.Api
{
    public static class ConfigurarAplicação
    {
        public static void Configurar(this IApplicationBuilder aplicação)
        {
            aplicação.UseMiddleware<MiddlewareDaUnidadeDeTrabalho>();
            aplicação.UseHttpsRedirection();
            aplicação.UseRouting();
            aplicação.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
