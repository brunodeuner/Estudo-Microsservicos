using Estudo.Core.Infraestrutura.Geral.Json.Abstrações;
using System.Text.Json;

namespace Estudo.Core.Infraestrutura.Geral.Json.SystemTextJson
{
    public class Serializador : ISerializador
    {
        public byte[] Serializar(object objeto) => JsonSerializer.SerializeToUtf8Bytes(objeto);
    }
}
