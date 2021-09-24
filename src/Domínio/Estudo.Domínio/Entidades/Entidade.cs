using Estudo.Infraestrutura.Armazenamento.Abstrações;

namespace Estudo.Domínio.Entidades
{
    public class Entidade : IId
    {
        public string Id { get; set; }
    }
}
