using System.Collections.Generic;

namespace Estudo.Core.Http.Testes.Dtos
{
    public class Cenário
    {
        public string Nome { get; set; }
        public IList<Comando> Comandos { get; set; }
    }
}
