using Estudo.Cobranças.Domínio.Entidades;
using Estudo.Core.Domínio.Validação;
using Estudo.Core.Infraestrutura.Armazenamento.Abstrações;
using Estudo.Core.Infraestrutura.Armazenamento.Abstrações.Queryable;
using Estudo.Core.Infraestrutura.Armazenamento.Abstrações.Repositório;
using Estudo.Core.Infraestrutura.Geral;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Estudo.Cobranças.Domínio.Repositórios
{
    public class RepositórioDeCobrança : Repositório<Cobrança>
    {
        private readonly IMediator mediator;

        public RepositórioDeCobrança(IDao dao, IMediator mediator) : base(dao) => this.mediator = mediator;

        public IAsyncEnumerable<Cobrança> ObterCobrançasDoClienteOuDoMês(CancellationToken cancellationToken,
            string cpf = default, int? mês = default)
        {
            if (cpf.NãoPreenchido() && !mês.HasValue)
            {
                mediator.Publish(new NotificaçãoDoDomínio($"Informe um {nameof(cpf)} ou um " +
                    $"{nameof(mês)}"));
                return default;
            }

            var query = Selecionar();
            if (cpf is not null)
                query = query.Where(x => x.Pessoa.Cpf == cpf);
            if (mês.HasValue)
                query = query.Where(x => x.DataDeVencimento.Month == mês);
            return query.ToAsyncEnumerable(cancellationToken);
        }
    }
}
