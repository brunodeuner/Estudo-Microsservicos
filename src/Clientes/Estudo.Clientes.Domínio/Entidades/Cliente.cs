using Estudo.Core.Domínio.Entidades;

namespace Estudo.Clientes.Domínio.Entidades
{
    public class Cliente : Entidade
    {
        public Cliente() { }

        public Cliente(string nome, string estado, string cpf)
        {
            Nome = nome;
            Estado = estado;
            Cpf = cpf;
        }

        public string Nome { get; init; }
        public string Estado { get; init; }
        public string Cpf { get; init; }
    }
}
