using Estudo.Domínio.Entidades;

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

        public string Nome { get; private set; }
        public string Estado { get; private set; }
        public string Cpf { get; private set; }
    }
}
