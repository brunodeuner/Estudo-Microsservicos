using BenchmarkDotNet.Attributes;

namespace Estudo.Testes.Consumo.Performance.Benchmarks
{
    [MemoryDiagnoser]
    public class ObtençãoDosDigitosDoValorAPartirDoCpf
    {
        private const string Cpf = "12345678901";

        [Benchmark]
        public string ConcatESubstring() => string.Concat(Cpf.Substring(0, 2), Cpf[^2..]);

        [Benchmark]
        public string InterpolaçãoESubString() => $"{Cpf.Substring(0, 2)}{Cpf[^2..]}";

        [Benchmark]
        public string ConcatEAcessoDiretoAsPosiçõesDoCpf() => string.Concat(Cpf[0], Cpf[1], Cpf[9], Cpf[10]);

        [Benchmark]
        public string InterpolaçãoEAcessoDiretoAsPosiçõesDoCpf() => $"{Cpf[0]}{Cpf[1]}{Cpf[9]}{Cpf[10]}";
    }
}
