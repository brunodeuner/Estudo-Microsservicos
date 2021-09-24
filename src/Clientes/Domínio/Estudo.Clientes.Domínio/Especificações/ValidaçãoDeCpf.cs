using DocsBr;
using Estudo.Clientes.Domínio.Entidades;

namespace Estudo.Clientes.Domínio.Especificações
{
    internal static class ValidaçãoDeCpf
    {
        public static bool ÉValido(this string cpf) => new CPF(cpf).IsValid();
        public static string MensagemDeCpfInválido => $"{nameof(Cliente.Cpf)} inválido";
        public static string MensagemDeCpfJáCadastrado => $"{nameof(Cliente.Cpf)} já cadastrado";
    }
}
