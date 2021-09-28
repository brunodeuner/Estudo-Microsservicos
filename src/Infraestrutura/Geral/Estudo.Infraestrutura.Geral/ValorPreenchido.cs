namespace Estudo.Infraestrutura.Geral
{
    public static class ValorPreenchido
    {
        public static bool Preenchido(this string valor) => !string.IsNullOrWhiteSpace(valor);

        public static bool Preenchido<T>(this T? valor) where T : struct => valor.HasValue;

        public static bool NãoPreenchido(this string valor) => !valor.Preenchido();

        public static bool NãoPreenchido<T>(this T? valor) where T : struct => !valor.Preenchido();
    }
}
