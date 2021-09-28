using Estudo.Domínio.Entidades;

namespace Estudo.Cobranças.Domínio.Entidades
{
    public class Pessoa : Entidade
    {
        public Pessoa() { }

        public Pessoa(string cpf, string estado)
        {
            Cpf = cpf;
            Estado = estado;
        }

        public string Cpf { get; init; }
        public string Estado { get; init; }
    }
}
