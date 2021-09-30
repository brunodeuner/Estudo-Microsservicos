using BenchmarkDotNet.Running;
using Estudo.CálculoDeConsumo.Testes.Performance.Benchmarks;

namespace Estudo.CálculoDeConsumo.Testes.Performance
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
