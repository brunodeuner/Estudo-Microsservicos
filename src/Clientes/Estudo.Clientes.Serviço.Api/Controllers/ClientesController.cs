using Estudo.Clientes.Domínio.CasosDeUso.Manutenção.ManipuladoresDeComandos.Comandos;
using Estudo.Clientes.Domínio.Entidades;
using Estudo.Clientes.Domínio.Especificações;
using Estudo.Clientes.Domínio.Repositórios;
using Estudo.Core.Domínio.Validação;
using Estudo.Core.Serviço.Api.Atributos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Clientes.Serviço.Api.Controllers
{
    [RotaPadrão]
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
        public async ValueTask<Cliente> ObterClienteAPartirDoCpf([FromQuery] string cpf,
            CancellationToken cancellationToken)
        {
            if (cpf.ÉValido())
                return await repositórioDeCliente.ObterAPartirDoCpf(cpf.ObterSomenteOsNúmeros(), cancellationToken);

            await mediator.Publish(new NotificaçãoDoDomínio(ValidaçãoDeCpf.MensagemDeCpfInválido),
                cancellationToken);
            return default;
        }

        [HttpGet("Todos")]
        public IAsyncEnumerable<Cliente> ObterTodos(CancellationToken cancellationToken) =>
            repositórioDeCliente.ObterTodos(cancellationToken);
    }
}