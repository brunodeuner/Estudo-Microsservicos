using Estudo.Infraestrutura.Armazenamento.Abstrações;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Serviço.Api.Middlewares
{
    internal class MiddlewareDaUnidadeDeTrabalho
    {
        private readonly RequestDelegate next;
        public MiddlewareDaUnidadeDeTrabalho(RequestDelegate next) => this.next = next;
        public async Task Invoke(HttpContext httpContext, IDao dao)
        {
            await next(httpContext);
            if (httpContext.Response.StatusCode < StatusCodes.Status400BadRequest)
                await dao.SalvarAlterações(httpContext.RequestAborted);
        }
    }
}
