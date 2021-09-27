using Estudo.Infraestrutura.Armazenamento.Abstrações;
using Estudo.Infraestrutura.Armazenamento.Abstrações.Queryable;
using Estudo.Infraestrutura.Geral;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Infraestrutura.Armazenamento.Memória
{
    /// <summary>
    /// Persiste os dados em memória, foi criado simplificado sem controle de unidade de trabalho
    /// </summary>
    public sealed partial class DaoMemória : IDao
    {
        private static readonly IDictionary<string, object> objetos = new ConcurrentDictionary<string, object>();

        public ValueTask Adicionar<T>(T objeto, CancellationToken cancellationToken) where T : class, new()
        {
            var id = objeto.ObterId();
            if (id.NãoPreenchido())
                id = objeto.AtribuirNovoId();
            objetos.Add(id, objeto);
            return ValueTask.CompletedTask;
        }

        public ValueTask Atualizar<T>(T objeto, CancellationToken cancellationToken) where T : class, new()
        {
            objetos[objeto.ObterId()] = objeto;
            return ValueTask.CompletedTask;
        }

        public ValueTask<T> ObterPeloId<T>(string id, CancellationToken cancellationToken)
        {
            if (objetos.TryGetValue(id, out var objeto))
                return ValueTask.FromResult((T)objeto);
            return default;
        }

        public ValueTask Remover<T>(string id, CancellationToken cancellationToken) where T : class, new()
        {
            objetos.Remove(id);
            return ValueTask.CompletedTask;
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
