using BenchmarkDotNet.Running;
using Estudo.Testes.CálculoDeConsumo.Performance.Benchmarks;

namespace Estudo.Testes.CálculoDeConsumo.Performance
{
    internal static class Program
    {
        public static void Main()
        {
            BenchmarkRunner.Run<TesteDePerformanceParaObtençãoDosDigitosDoValorAPartirDoCpf>();
            BenchmarkRunner.Run<TesteDePerformanceParaParseDeStringDeQuatroDigitosParaInteiro>();
            BenchmarkRunner.Run<TesteDePerformanceParaProcessamentoDeListaDeTarefas>();
        }
    }
}
