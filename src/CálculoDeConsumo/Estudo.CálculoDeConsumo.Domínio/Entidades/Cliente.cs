using Estudo.Domínio.Entidades;

namespace Estudo.CálculoDeConsumo.Domínio.Entidades
{
    public class Cliente : Entidade
    {
        public Cliente() { }

        public Cliente(string cpf) => Cpf = cpf;

        public string Cpf { get; init; }
    }
}
