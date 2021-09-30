using Estudo.CálculoDeConsumo.Domínio.CasosDeUso.ManipuladoresDeComandos.Comandos;
using Estudo.CálculoDeConsumo.Domínio.Entidades;
using Estudo.CálculoDeConsumo.Domínio.Repositórios;
using Estudo.CálculoDeConsumo.Infraestrutura;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.CálculoDeConsumo.Domínio.CasosDeUso.ManipuladoresDeComandos
{
    internal class ManipuladorDoComandoDeCobrarTodosOsClientes : IRequestHandler<ComandoDeCobrarTodosOsClientes>
    {
        private readonly RepositórioDeCliente repositórioDeCliente;
        private readonly RepositórioDeCobrança repositórioDeCobrança;

        public ManipuladorDoComandoDeCobrarTodosOsClientes(RepositórioDeCliente repositórioDeCliente,
            RepositórioDeCobrança repositórioDeCobrança)
        {
            this.repositórioDeCliente = repositórioDeCliente;
            this.repositórioDeCobrança = repositórioDeCobrança;
        }

        public async Task<Unit> Handle(ComandoDeCobrarTodosOsClientes request, CancellationToken cancellationToken)
        {
            var dataDeHoje = DateTime.UtcNow.Date;
            await ProcessarRegistrosEmLote.Processar(repositórioDeCliente.ObterTodos(cancellationToken),
                (cliente, cancellationToken) => repositórioDeCobrança.Salvar(
                    Cobrança.CriarComValorDeCobrançaAPartirDoCpf(cliente.Cpf, dataDeHoje),
                    cancellationToken).AsTask(), cancellationToken);
            return default;
        }
    }
}
