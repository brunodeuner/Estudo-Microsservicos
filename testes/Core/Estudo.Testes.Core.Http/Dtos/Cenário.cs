using System.Collections.Generic;

namespace Estudo.Testes.Core.Http.Dtos
{
    public class Cenário
    {
        public string Nome { get; set; }
        public IList<Comando> Comandos { get; set; }
    }
}
