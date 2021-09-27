using Estudo.CálculoDeConsumo.Domínio.CasosDeUso.ManipuladoresDeComandos.Comandos;
using Estudo.CálculoDeConsumo.Domínio.Repositórios;
using MediatR;
using System;
using System.Collections.Generic;
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
            var hoje = DateTime.UtcNow.Date;
            var listaDeCobrançasSendoSalvas = new List<Task>();
            await foreach (var cliente in repositórioDeCliente.ObterTodos(cancellationToken))
            {
                listaDeCobrançasSendoSalvas.RemoveAll(x => x.IsCompleted);
                listaDeCobrançasSendoSalvas.Add(
                    repositórioDeCobrança.Salvar(new Entidades.Cobrança(cliente.Cpf, hoje), cancellationToken).AsTask());
            }
            await Task.WhenAll(listaDeCobrançasSendoSalvas);
            return default;
        }
    }
}
