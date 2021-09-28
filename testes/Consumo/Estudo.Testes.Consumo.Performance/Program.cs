using BenchmarkDotNet.Running;
using Estudo.Testes.Consumo.Performance.Benchmarks;

namespace Estudo.Testes.Consumo.Performance
{
    internal static class Program
    {
        public static void Main()
        {
            //BenchmarkRunner.Run<ObtençãoDosDigitosDoValorAPartirDoCpf>();
            //BenchmarkRunner.Run<ParseDeStringParaOValorDeCobrança>();
            BenchmarkRunner.Run<ProcessamentoDeListaDeTarefas>();
        }
    }
}
