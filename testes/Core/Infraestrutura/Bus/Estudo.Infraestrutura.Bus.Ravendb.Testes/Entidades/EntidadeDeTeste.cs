using Estudo.Core.Infraestrutura.Armazenamento.Abstrações;

namespace Estudo.Core.Infraestrutura.Bus.Ravendb.Testes.Entidades
{
    internal class EntidadeDeTeste : IId
    {
        public string Id { get; set; }
        public string Descrição { get; set; }
    }
}
