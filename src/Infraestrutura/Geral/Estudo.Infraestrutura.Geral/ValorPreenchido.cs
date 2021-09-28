namespace Estudo.Infraestrutura.Geral
{
    public static class ValorPreenchido
    {
        public static bool Preenchido(this string valor) => !string.IsNullOrWhiteSpace(valor);

        public static bool NãoPreenchido(this string valor) => !valor.Preenchido();
    }
}
