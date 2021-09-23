using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Infraestrutura.Armazenamento.Abstrações.Repositório
{
    public class Repositório<T> : IRepositório<T> where T : class, new()
    {
        private readonly IDao dao;

        public virtual Task<T> ObterPeloId(string id, CancellationToken cancellationToken) =>
            dao.ObterPeloId<T>(id, cancellationToken);

        public virtual Task Remover(T objeto, CancellationToken cancellationToken) =>
            dao.Remover(objeto, cancellationToken);

        public virtual Task Salvar(T objeto, CancellationToken cancellationToken)
        {
            if (objeto.IdPreenchido())
                return AtualizarAsync(objeto, cancellationToken);
            return AdicionarAsync(objeto, cancellationToken);
        }

        protected IQueryable<T> Selecionar() => dao.Selecionar<T>();

        protected virtual Task AdicionarAsync(T objeto, CancellationToken cancellationToken) =>
            dao.Adicionar(objeto, cancellationToken);

        protected virtual Task AtualizarAsync(T objeto, CancellationToken cancellationToken) =>
            dao.Atualizar(objeto, cancellationToken);
    }
}
