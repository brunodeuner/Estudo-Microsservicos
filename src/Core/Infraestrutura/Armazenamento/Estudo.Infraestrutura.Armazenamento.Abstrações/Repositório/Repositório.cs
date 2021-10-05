using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Core.Infraestrutura.Armazenamento.Abstrações.Repositório
{
    public class Repositório<T> : IRepositório<T> where T : class, new()
    {
        private readonly IDao dao;

        public Repositório(IDao dao) => this.dao = dao;

        public virtual ValueTask<T> ObterPeloId(string id, CancellationToken cancellationToken) =>
            dao.ObterPeloId<T>(id, cancellationToken);

        public virtual ValueTask Salvar(T entidade, CancellationToken cancellationToken) =>
            dao.Salvar(entidade, cancellationToken);

        public virtual ValueTask Remover(T entidade, CancellationToken cancellationToken) =>
            dao.Remover(entidade, cancellationToken);

        protected IQueryable<T> Selecionar() => dao.Selecionar<T>();
    }
}
