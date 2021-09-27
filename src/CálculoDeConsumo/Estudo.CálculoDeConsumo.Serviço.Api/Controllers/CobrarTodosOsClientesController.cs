using Estudo.CálculoDeConsumo.Domínio.CasosDeUso.ManipuladoresDeComandos.Comandos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.CálculoDeConsumo.Serviço.Api.Controllers
{
    [Route("[controller]")]
    public class CobrarTodosOsClientesController
    {
        private readonly IMediator mediator;

        public CobrarTodosOsClientesController(IMediator mediator) => this.mediator = mediator;

        [HttpPost]
        public Task CobrarCliente(CancellationToken cancellationToken) =>
            mediator.Send(new ComandoDeCobrarTodosOsClientes(), cancellationToken);
    }
}
