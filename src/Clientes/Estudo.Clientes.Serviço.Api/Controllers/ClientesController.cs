using Estudo.Clientes.Domínio.CasosDeUso.Manutenção.ManipuladoresDeComandos.Comandos;
using Estudo.Clientes.Domínio.Entidades;
using Estudo.Clientes.Domínio.Repositórios;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Clientes.Serviço.Api.Controllers
{
    [Route("[controller]")]
    public class ClientesController
    {
        private readonly IMediator mediator;
        private readonly RepositórioDeCliente repositórioDeCliente;

        public ClientesController(IMediator mediator, RepositórioDeCliente repositórioDeCliente)
        {
            this.mediator = mediator;
            this.repositórioDeCliente = repositórioDeCliente;
        }

        [HttpPost]
        public Task AdicionarCliente([FromBody] ComandoDeAdicionarCliente comandoDeCobrarCliente,
            CancellationToken cancellationToken) => mediator.Send(comandoDeCobrarCliente, cancellationToken);

        [HttpGet]
        public ValueTask<Cliente> ObterClienteAPartirDoCpf([FromQuery] string cpf,
            CancellationToken cancellationToken) => repositórioDeCliente.ObterAPartirDoCpf(cpf, cancellationToken);
    }
}