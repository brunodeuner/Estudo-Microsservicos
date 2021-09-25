using Estudo.Cobranças.Domínio.CasosDeUso.CobrarCliente.ManipuladoresDeComandos.Comandos;
using Estudo.Cobranças.Domínio.Entidades;
using Estudo.Cobranças.Domínio.Repositórios;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Cobranças.Serviço.Api.Controllers
{
    [Route("[controller]")]
    //[ValidateModel]
    public class CobrarClienteController
    {
        private readonly IMediator mediator;
        private readonly RepositórioDeCobrança repositórioDeCobrança;

        //public CobrarClienteController(IMediator bus, RepositórioDeCobrança repositórioDeCobrança)
        //{
        //    this.bus = bus;
        //    this.repositórioDeCobrança = repositórioDeCobrança;
        //}

        [HttpPost]
        public Task CobrarCliente(ComandoDeCobrarCliente comandoDeCobrarCliente, CancellationToken cancellationToken)
            => mediator.Send(comandoDeCobrarCliente, cancellationToken);

        [HttpGet]
        public IAsyncEnumerable<Cobrança> ObterCobranças([FromQuery] string cpf, [FromQuery] int? mês,
            CancellationToken cancellationToken) => repositórioDeCobrança
            .ObterCobrançasDoClienteAPartirDoMês(cancellationToken, cpf, mês);
    }

    //public class Parametros
    //{
    //    public string Cpf { get; init; }
    //    public int? Mês { get; init; }

    //    //public bool InformouAoMenosUmParametro() => Cpf is null && !Mês.HasValue;
    //}

    //public class ValidateModelAttribute : ActionFilterAttribute
    //{
    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {
    //        if (!filterContext.ModelState.IsValid)
    //            filterContext.Result = new BadRequestObjectResult(filterContext.ModelState);
    //    }
    //}
}