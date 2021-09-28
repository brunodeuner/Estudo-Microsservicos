using Estudo.Infraestrutura.Armazenamento.Abstrações;

namespace Estudo.Infraestrutura.Armazenamento.HttpClient.Testes
{
    internal class EntidadeDeTeste : IId
    {
        public string Id { get; set; }
        public string Descrição { get; set; }
    }
}
