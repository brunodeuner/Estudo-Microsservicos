using Estudo.Domínio.Entidades;

namespace Estudo.Cobranças.Domínio.Entidades
{
    public class Pessoa : Entidade
    {
        public Pessoa() { }

        public Pessoa(string cpf) => Cpf = cpf;

        public string Cpf { get; init; }
    }
}
