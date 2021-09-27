using Estudo.Clientes.Domínio.CasosDeUso.Manutenção.ManipuladoresDeComandos.Comandos;
using Estudo.Clientes.Domínio.Entidades;
using Estudo.Clientes.Domínio.Repositórios;
using Estudo.Domínio.Validadores;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Clientes.Domínio.CasosDeUso.Manutenção.ManipuladoresDeComandos
{
    internal class ManipuladorDoComandoDeAdicionarCliente :
        IRequestHandler<ComandoDeAdicionarCliente>,
        IRequestHandler<ComandoDeRemoverCliente>
    {
        private readonly IValidador validador;
        private readonly RepositórioDeCliente repositórioDeCliente;

        public ManipuladorDoComandoDeAdicionarCliente(IValidador validador, RepositórioDeCliente repositórioDeCliente)
        {
            this.validador = validador;
            this.repositórioDeCliente = repositórioDeCliente;
        }

        public async Task<Unit> Handle(ComandoDeAdicionarCliente request, CancellationToken cancellationToken)
        {
            if (!await validador.Validar(request, cancellationToken))
                return default;

            var novoCliente = new Cliente(request.Nome, request.Estado, request.Cpf);
            await repositórioDeCliente.Salvar(novoCliente, cancellationToken);
            return default;
        }

        public async Task<Unit> Handle(ComandoDeRemoverCliente request, CancellationToken cancellationToken)
        {
            if (!await validador.Validar(request, cancellationToken))
                return default;

            await repositórioDeCliente.Remover(request.Id, cancellationToken);
            return default;
        }
    }
}
