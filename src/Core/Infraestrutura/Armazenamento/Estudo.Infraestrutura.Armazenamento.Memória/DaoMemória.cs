using Estudo.Core.Infraestrutura.Armazenamento.Abstrações;
using Estudo.Core.Infraestrutura.Armazenamento.Abstrações.Queryable;
using Estudo.Core.Infraestrutura.Geral;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Core.Infraestrutura.Armazenamento.Memória
{
    /// <summary>
    /// Persiste os dados em memória, foi criado simplificado sem controle de unidade de trabalho
    /// </summary>
    public sealed partial class DaoMemória : IDao
    {
        private static readonly IDictionary<string, object> objetos = new ConcurrentDictionary<string, object>();

        public ValueTask Salvar<T>(T objeto, CancellationToken cancellationToken) where T : class, new()
        {
            var id = objeto.ObterId();
            if (id.NãoPreenchido())
                id = objeto.AtribuirNovoId();
            objetos[id] = objeto;
            return ValueTask.CompletedTask;
        }

        public ValueTask Remover<T>(T objeto, CancellationToken cancellationToken) where T : class, new()
        {
            objetos.Remove(objeto.ObterId());
            return ValueTask.CompletedTask;
        }

        public ValueTask<T> ObterPeloId<T>(string id, CancellationToken cancellationToken)
        {
            if (objetos.TryGetValue(id, out var objeto))
                return ValueTask.FromResult((T)objeto);
            return default;
        }

        public ValueTask SalvarAlterações(CancellationToken cancellationToken) => ValueTask.CompletedTask;

        public IQueryable<T> Selecionar<T>()
        {
            var tipoAFiltrar = typeof(T);
            var query = objetos
                .Where(x => x.Value.GetType() == tipoAFiltrar)
                .Select(x => x.Value).Cast<T>().AsQueryable();
            return new QueryableDao<T>(query, new QueryProviderDao(query.Provider, this));
        }
    }
}
