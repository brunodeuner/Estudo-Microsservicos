using Estudo.Cobranças.Domínio.CasosDeUso.CobrarCliente.ManipuladoresDeComandos.Comandos;
using Estudo.Cobranças.Domínio.Entidades;
using Estudo.Cobranças.Domínio.Repositórios;
using Estudo.Domínio.Validadores;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Cobranças.Domínio.CasosDeUso.CobrarCliente.ManipuladoresDeComandos
{
    public class ManipuladorDoComandoDeCobrarCliente :
        IRequestHandler<ComandoDeCobrarCliente>,
        IRequestHandler<ComandoDeRemoverCobrança>
    {
        private readonly IValidador validador;
        private readonly RepositórioDeCobrança repositórioDeCobrança;
        private readonly RepositórioDePessoa repositórioDePessoa;

        public ManipuladorDoComandoDeCobrarCliente(IValidador validador,
            RepositórioDeCobrança repositórioDeCobrança, RepositórioDePessoa repositórioDePessoa)
        {
            this.validador = validador;
            this.repositórioDeCobrança = repositórioDeCobrança;
            this.repositórioDePessoa = repositórioDePessoa;
        }

        public async Task<Unit> Handle(ComandoDeCobrarCliente request, CancellationToken cancellationToken)
        {
            if (!await validador.Validar(request, cancellationToken))
                return default;

            var pessoa = await repositórioDePessoa.ObterAPartirDoCpf(request.Cpf, cancellationToken);
            var novaCobrança = new Cobrança(pessoa, request.DataDeVencimento, request.Valor);
            await repositórioDeCobrança.Salvar(novaCobrança, cancellationToken);
            return default;
        }

        public async Task<Unit> Handle(ComandoDeRemoverCobrança request, CancellationToken cancellationToken)
        {
            if (!await validador.Validar(request, cancellationToken))
                return default;

            await repositórioDeCobrança.Remover(request.Id, cancellationToken);
            return default;
        }
    }
}
