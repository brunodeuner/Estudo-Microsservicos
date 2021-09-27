using System.Net;

namespace Estudo.Testes.Core.Http.Dtos
{
    public class Retorno
    {
        public HttpStatusCode Status { get; set; }
        public object Corpo { get; set; }
    }
}
