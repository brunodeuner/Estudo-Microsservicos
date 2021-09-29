using Estudo.Core.Infraestrutura.Armazenamento.Abstrações;
using System.Net.Http;

namespace Estudo.Core.Infraestrutura.Armazenamento.HttpClient.Dtos
{
    public struct DadosDaRequisição<T>
    {
        public DadosDaRequisição(HttpMethod httpMethod, object corpo)
        {
            HttpMethod = httpMethod;
            Corpo = corpo;
            Id = corpo.ObterId();
        }

        public DadosDaRequisição(HttpMethod httpMethod, string id) : this()
        {
            HttpMethod = httpMethod;
            Id = id;
        }

        public DadosDaRequisição(HttpMethod httpMethod) : this()
        {
            HttpMethod = httpMethod;
        }

        public HttpMethod HttpMethod { get; init; }
        public object Corpo { get; init; }
        public string Id { get; init; }
    }
}
