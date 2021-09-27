using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estudo.Testes.Consumo.Performance.Benchmarks
{
    [MemoryDiagnoser]
    public class ProcessamentoDeListaDeTarefas
    {
        private const int QuantidadeDeTarefasACriar = 2000;

        [Benchmark]
        public async Task CriarTaskListEEliminarContinueWithA()
        {
            var objetoParaBloqueio = new object();
            var lista = new List<Task>();
            for (var i = 0; i <= QuantidadeDeTarefasACriar; i++)
                lista.Add(ObterTarefa().AsTask().ContinueWith(x =>
                {
                    lock (objetoParaBloqueio)
                        lista.Remove(x);
                }));
            await Task.WhenAll(lista);
        }

        [Benchmark]
        public async Task CriarTaskListEEliminarContinueWithB()
        {
            var lista = new SynchronizedCollection<Task>();
            for (var i = 0; i <= QuantidadeDeTarefasACriar; i++)
                lista.Add(ObterTarefa().AsTask().ContinueWith(x => lista.Remove(x)));
            await Task.WhenAll(lista);
        }

        [Benchmark]
        public async Task CriarTaskListEEliminarManual()
        {
            var lista = new List<Task>();
            for (var i = 0; i <= QuantidadeDeTarefasACriar; i++)
            {
                lista.RemoveAll(x => x.IsCompleted);
                lista.Add(ObterTarefa().AsTask());
            }
            await Task.WhenAll(lista);
        }

        [Benchmark]
        public async Task CriarTaskListEEliminarManualB()
        {
            var lista = new List<ValueTask>();
            for (var i = 0; i <= QuantidadeDeTarefasACriar; i++)
            {
                lista.RemoveAll(x => x.IsCompleted);
                lista.Add(ObterTarefa());
            }
            await Task.WhenAll(lista.Where(x => !x.IsCompleted).Select(x => x.AsTask()));
        }

        private static async ValueTask ObterTarefa() => await Task.Delay(10);
    }
}
