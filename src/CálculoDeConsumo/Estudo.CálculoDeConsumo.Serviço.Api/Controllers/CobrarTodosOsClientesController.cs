using Estudo.CálculoDeConsumo.Domínio.CasosDeUso.ManipuladoresDeComandos.Comandos;
using Estudo.Core.Serviço.Api.Atributos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.CálculoDeConsumo.Serviço.Api.Controllers
{
    [RotaPadrão]
    public class CobrarTodosOsClientesController
    {
        private readonly IMediator mediator;

        public CobrarTodosOsClientesController(IMediator mediator) => this.mediator = mediator;

        [HttpPost]
        public Task CobrarCliente(CancellationToken cancellationToken) =>
            mediator.Send(new ComandoDeCobrarTodosOsClientes(), cancellationToken);
    }
}
