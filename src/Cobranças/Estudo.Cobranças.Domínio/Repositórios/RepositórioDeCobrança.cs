using Estudo.Cobranças.Domínio.Entidades;
using Estudo.Domínio.Validação;
using Estudo.Infraestrutura.Armazenamento.Abstrações.Repositório;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Estudo.Cobranças.Domínio.Repositórios
{
    public class RepositórioDeCobrança : Repositório<Cobrança>
    {
        private readonly IMediator mediator;

        public RepositórioDeCobrança(IMediator mediator) => this.mediator = mediator;

        public IAsyncEnumerable<Cobrança> ObterCobrançasDoClienteAPartirDoMês(CancellationToken cancellationToken,
            string cpf = default, int? mês = default)
        {
            if (cpf.Preenchido() && !mês.HasValue)
            {
                mediator.Publish(new NotificaçãoDoDomínio($"Informe um {nameof(cpf)} ou um " +
                    $"{nameof(mês)}"));
                return default;
            }

            var query = Selecionar();
            if (cpf is not null)
                query = query.Where(x => x.Cpf == cpf);
            if (mês.HasValue)
                query = query.Where(x => x.DataDeVencimento.Month == mês);
            return query.ToAsyncEnumerable(cancellationToken);
        }
    }
}
