using Estudo.Domínio.Comandos;

namespace Estudo.Clientes.Domínio.CasosDeUso.Manutenção.ManipuladoresDeComandos.Comandos
{
    public class ComandoDeAdicionarCliente : ComandoSemRetorno
    {
        public string Nome { get; set; }
        public string Estado { get; set; }
        public string Cpf { get; set; }
    }
}
