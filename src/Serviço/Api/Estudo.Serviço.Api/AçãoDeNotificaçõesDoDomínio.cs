using Estudo.Domínio.Validação;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Estudo.Serviço.Api
{
    public class AçãoDeNotificaçõesDoDomínio : IActionFilter
    {
        private readonly INotificaçõesDoDomínio notificaçõesDoDomínio;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Canceled || context.ExceptionHandled)
                return;

            if (notificaçõesDoDomínio.PossuiNotificações())
            {
                context.Result = new JsonResult(notificaçõesDoDomínio.ObterNotificações());
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context) { }
    }
}
