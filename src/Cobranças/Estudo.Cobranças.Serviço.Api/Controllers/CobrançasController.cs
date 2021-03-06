using Estudo.Cobranças.Domínio.CasosDeUso.CobrarPessoa.ManipuladoresDeComandos.Comandos;
using Estudo.Cobranças.Domínio.Entidades;
using Estudo.Cobranças.Domínio.Repositórios;
using Estudo.Core.Serviço.Api.Atributos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Cobranças.Serviço.Api.Controllers
{
    [RotaPadrão]
    public class CobrançasController
    {
        private readonly IMediator mediator;
        private readonly RepositórioDeCobrança repositórioDeCobrança;

        public CobrançasController(IMediator mediator, RepositórioDeCobrança repositórioDeCobrança)
        {
            this.mediator = mediator;
            this.repositórioDeCobrança = repositórioDeCobrança;
        }

        [HttpPost]
        public Task CobrarCliente([FromBody] ComandoDeCobrarPessoa comandoDeCobrarCliente,
            CancellationToken cancellationToken) => mediator.Send(comandoDeCobrarCliente, cancellationToken);

        [HttpGet]
        public IAsyncEnumerable<Cobrança> ObterCobranças([FromQuery] string cpf, [FromQuery] int? mês,
            CancellationToken cancellationToken) => repositórioDeCobrança
            .ObterCobrançasDoClienteOuDoMês(cancellationToken, cpf, mês);
    }
}