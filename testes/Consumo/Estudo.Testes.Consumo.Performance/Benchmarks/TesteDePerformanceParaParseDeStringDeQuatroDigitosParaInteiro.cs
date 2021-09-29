using BenchmarkDotNet.Attributes;

namespace Estudo.CálculoDeConsumo.Testes.Performance.Benchmarks
{
    [MemoryDiagnoser]
    public class TesteDePerformanceParaParseDeStringDeQuatroDigitosParaInteiro
    {
        private const string CpfProcessado = "1234";

        [Benchmark]
        public int ParseDeInteiro() => int.Parse(CpfProcessado);

        [Benchmark]
        public decimal ParseDeDoubleEConvertidoParaDecimal() => (decimal)double.Parse(CpfProcessado);

        [Benchmark]
        public decimal ParseDeDecimal() => decimal.Parse(CpfProcessado);
    }
}
