using Estudo.Core.Infraestrutura.Bus.Abstrações;
using Estudo.Core.Infraestrutura.Bus.Abstrações.Produtor;
using Estudo.Core.Infraestrutura.Bus.Memória.Consumidor;
using System.Threading;
using System.Threading.Tasks;

namespace Estudo.Core.Infraestrutura.Bus.Memória.Produtor
{
    public class ProdutorEmMemória : IProdutor
    {
        private readonly EventosPorTipo eventosPorTipo;

        public ProdutorEmMemória(EventosPorTipo eventosPorTipo) => this.eventosPorTipo = eventosPorTipo;

        public Task EnviarAsync<TMensagem>(string identificador, EventoEventArgs<TMensagem> requisicao,
            CancellationToken cancellationToken)
        {
            eventosPorTipo.AdicionarEvento(identificador, requisicao);
            return Task.CompletedTask;
        }
    }
}
