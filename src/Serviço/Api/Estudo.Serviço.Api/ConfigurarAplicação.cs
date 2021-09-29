using Estudo.Core.Serviço.Api.Middlewares;
using Microsoft.AspNetCore.Builder;
using System.Reflection;

namespace Estudo.Core.Serviço.Api
{
    public static class ConfigurarAplicação
    {
        public static void Configurar(this IApplicationBuilder aplicação)
        {
            aplicação.UseMiddleware<MiddlewareDaUnidadeDeTrabalho>();
            aplicação.UseHttpsRedirection();
            aplicação.AdicionarSwagger();
            aplicação.UseRouting();
            aplicação.UseEndpoints(endpoints => endpoints.MapControllers());
        }

        private static void AdicionarSwagger(this IApplicationBuilder aplicação)
        {
            aplicação.UseSwagger();
            aplicação.UseSwaggerUI(c =>
            c.SwaggerEndpoint("/swagger/v1/swagger.json",
                Assembly.GetExecutingAssembly().GetName().Name));
        }
    }
}
