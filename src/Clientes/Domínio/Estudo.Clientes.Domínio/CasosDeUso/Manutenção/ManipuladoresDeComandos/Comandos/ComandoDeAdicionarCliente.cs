using Estudo.Clientes.Domínio.CasosDeUso.Manutenção.ManipuladoresDeComandos.Comandos.Validadores;
using Estudo.Clientes.Domínio.Especificações;
using Estudo.Domínio.Comandos;
using Estudo.Domínio.Validadores;

namespace Estudo.Clientes.Domínio.CasosDeUso.Manutenção.ManipuladoresDeComandos.Comandos
{
    [Validador(typeof(ValidadorDeDadosDoComandoDeAdicionarCliente))]
    [Validador(typeof(ValidadorDeCpfDuplicadoDoComandoDeAdicionarCliente))]
    public class ComandoDeAdicionarCliente : ComandoSemRetorno
    {
        private string cpf;

        public string Nome { get; init; }
        public string Estado { get; init; }
        public string Cpf { get => cpf; init => cpf = value.ObterSomenteOsNúmeros(); }
    }
}
