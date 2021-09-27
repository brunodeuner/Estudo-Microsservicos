using Estudo.Domínio.Comandos;
using Estudo.Infraestrutura.Armazenamento.Abstrações;

namespace Estudo.Cobranças.Domínio.CasosDeUso.CobrarCliente.ManipuladoresDeComandos.Comandos
{
    public class ComandoDeRemoverCobrança : ComandoSemRetorno, IId
    {
        public ComandoDeRemoverCobrança(string id) => Id = id;

        public string Id { get; init; }
    }
}
