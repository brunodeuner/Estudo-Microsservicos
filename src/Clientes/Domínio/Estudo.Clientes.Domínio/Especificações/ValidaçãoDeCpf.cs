using DocsBr;
using Estudo.Clientes.Domínio.Entidades;
using Estudo.Infraestrutura.Geral;

namespace Estudo.Clientes.Domínio.Especificações
{
    internal static class ValidaçãoDeCpf
    {
        public static bool ÉValido(this string cpf) => cpf.Preenchido() && new CPF(cpf).IsValid();
        public static string ObterSomenteOsNúmeros(this string cpf) => new CPF(cpf).SemMascara();
        public static string MensagemDeCpfInválido => $"{nameof(Cliente.Cpf)} inválido";
        public static string MensagemDeCpfJáCadastrado => $"{nameof(Cliente.Cpf)} já cadastrado";
    }
}
