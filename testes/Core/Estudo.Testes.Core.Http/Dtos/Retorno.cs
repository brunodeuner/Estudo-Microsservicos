using System.Net;

namespace Estudo.Core.Http.Testes.Dtos
{
    public class Retorno
    {
        public HttpStatusCode Status { get; set; }
        public object Corpo { get; set; }
    }
}
