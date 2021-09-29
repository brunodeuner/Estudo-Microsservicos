using Estudo.Core.Domínio.Validação;
using Estudo.Core.Infraestrutura.Geral;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Net;

namespace Estudo.Core.Serviço.Api.Filtros
{
    public class FiltroDeNotificaçõesDoDomínio : IActionFilter
    {
        private readonly INotificaçõesDoDomínio notificaçõesDoDomínio;

        public FiltroDeNotificaçõesDoDomínio(INotificaçõesDoDomínio notificaçõesDoDomínio) =>
            this.notificaçõesDoDomínio = notificaçõesDoDomínio;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Canceled || context.ExceptionHandled)
                return;

            if (notificaçõesDoDomínio.PossuiNotificações())
            {
                var notificações = notificaçõesDoDomínio.ObterNotificações();
                var mensagens = notificações.Where(x => x.Descrição.Preenchido()).Select(x => x.Descrição)
                    .Concat(notificações
                        .Where(x => x.ResultadoDaValidação is not null)
                        .SelectMany(x => x.ResultadoDaValidação.Errors.Select(x => x.ErrorMessage)));

                context.Result = new JsonResult(mensagens);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // nada a executar
        }
    }
}
