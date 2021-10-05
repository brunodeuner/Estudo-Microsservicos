using Estudo.Core.Infraestrutura.Armazenamento.Abstrações;

namespace Estudo.Core.Infraestrutura.Armazenamento.Ravendb.Testes.Entidades
{
    internal class EntidadeDeTeste : IId
    {
        public string Id { get; set; }
        public string Descrição { get; set; }
    }
}
