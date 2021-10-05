using Estudo.Core.Domínio.Eventos.Manutenção;
using Estudo.Core.Infraestrutura.Armazenamento.Abstrações;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Core.Domínio.Repositórios
{
    public class Repositório<T> : Infraestrutura.Armazenamento.Abstrações.Repositório.Repositório<T> where T : class, new()
    {
        private readonly IMediator mediator;

        public Repositório(IDao dao, IMediator mediator) : base(dao) => this.mediator = mediator;

        public override async ValueTask Salvar(T entidade, CancellationToken cancellationToken)
        {
            await base.Salvar(entidade, cancellationToken);
            await mediator.Publish(new EventoDeEntidadeSalva<T>(entidade), cancellationToken);
        }

        public override async ValueTask Remover(T entidade, CancellationToken cancellationToken)
        {
            await base.Remover(entidade, cancellationToken);
            await mediator.Publish(new EventoDeEntidadeRemovida<T>(entidade), cancellationToken);
        }
    }
}
