using Estudo.Infraestrutura.Armazenamento.Abstrações;

namespace Estudo.Infraestrutura.Bus.Ravendb.Testes.Entidades
{
    internal class EntidadeDeTeste : IId
    {
        public string Id { get; set; }
        public string Descrição { get; set; }
    }
}
