using Estudo.Core.Infraestrutura.Armazenamento.Abstrações;

namespace Estudo.Core.Domínio.Entidades
{
    public class Entidade : IId
    {
        public string Id { get; set; }
    }
}
