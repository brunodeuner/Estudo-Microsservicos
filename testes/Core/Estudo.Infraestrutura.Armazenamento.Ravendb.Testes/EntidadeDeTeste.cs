using Estudo.Infraestrutura.Armazenamento.Abstrações;

namespace Estudo.Infraestrutura.Armazenamento.Ravendb.Testes
{
    internal class EntidadeDeTeste : IId
    {
        public string Id { get; set; }
        public string Descrição { get; set; }
    }
}
