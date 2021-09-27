using Estudo.Domínio.Comandos;
using Estudo.Infraestrutura.Armazenamento.Abstrações;

namespace Estudo.Clientes.Domínio.CasosDeUso.Manutenção.ManipuladoresDeComandos.Comandos
{
    public class ComandoDeRemoverCliente : ComandoSemRetorno, IId
    {
        public ComandoDeRemoverCliente(string id) => Id = id;

        public string Id { get; init; }
    }
}
